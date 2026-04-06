import json
import os
import re
import threading
from concurrent.futures import ThreadPoolExecutor, as_completed
from pathlib import Path
from typing import Dict, List

from openai import OpenAI

client = OpenAI(api_key=os.environ.get("OPEN_API_KEY"))

INPUT_FILE = "words.txt"
OUTPUT_KEEP = "words_cleaned.txt"
OUTPUT_REMOVE = "words_removed.txt"
OUTPUT_REVIEW = "words_review.txt"

MODEL = "gpt-4.1-nano"
BATCH_SIZE = 300
MAX_WORKERS = 6

COMMON_SHORT_WORDS = {
    "an", "as", "at", "be", "by", "do",
    "go", "he", "if", "in", "is", "it", "me",
    "my", "no", "of", "on", "or", "so", "to",
    "up", "us", "we"
}

PRINT_LOCK = threading.Lock()


def log(message: str) -> None:
    with PRINT_LOCK:
        print(message, flush=True)


def read_words(path: str) -> List[str]:
    text = Path(path).read_text(encoding="utf-8")
    words = [line.strip() for line in text.splitlines() if line.strip()]

    seen = set()
    unique = []
    for w in words:
        if w not in seen:
            seen.add(w)
            unique.append(w)
    return unique


def write_list(path: str, items: List[str]) -> None:
    Path(path).write_text("\n".join(items) + "\n", encoding="utf-8")


def chunked(items: List[str], size: int) -> List[List[str]]:
    return [items[i:i + size] for i in range(0, len(items), size)]


def obvious_remove(word: str) -> bool:
    w = word.strip()

    if not w:
        return True

    # Allow known common short words, reject most other 1-2 char tokens
    if len(w) <= 2:
        return w.lower() not in COMMON_SHORT_WORDS

    # Common abbreviation pattern
    if w.isupper() and len(w) <= 6:
        return True

    # Reject anything with characters outside letters and hyphen
    if re.search(r"[^a-zA-Z-]", w):
        return True

    # Repeated letters like aaa, bbbb
    if re.fullmatch(r"(.)\1{2,}", w):
        return True

    # Vowel-only junk like aa, aei
    if re.fullmatch(r"[aeiou]{2,}", w.lower()):
        return True

    return False


def indices_to_words(words: List[str], indices: List[int]) -> List[str]:
    return [words[i] for i in indices]


def classify_batch(words: List[str]) -> Dict[str, List[int]]:
    schema = {
        "type": "object",
        "properties": {
            "keep": {
                "type": "array",
                "items": {"type": "integer"}
            },
            "remove": {
                "type": "array",
                "items": {"type": "integer"}
            }
        },
        "required": ["keep", "remove"],
        "additionalProperties": False
    }

    indexed_words = [{"index": i, "word": word} for i, word in enumerate(words)]

    prompt = f"""
You are filtering a word list VERY aggressively.

Goal:
Keep only meaningful English words.

CRITICAL:
Default action is KEEP.

REMOVE if ANY apply:
- Length <= 3 unless extremely common (e.g., "the", "and")
- Repeated letters (aaa, bbbb)
- Interjections or sounds (aah, ah, oh, hmm, uh, um)
- Example of words to REMOVE: aah, abn, abo, abs, aarrghh, abbr, abp, bac, aam

IMPORTANT:
- Return only indices
- Every index must appear exactly once
- Do not invent or omit

Items:
{json.dumps(indexed_words, ensure_ascii=False)}
""".strip()

    response = client.responses.create(
        model=MODEL,
        input=prompt,
        text={
            "format": {
                "type": "json_schema",
                "name": "word_filter",
                "schema": schema,
                "strict": True
            }
        }
    )

    raw = json.loads(response.output_text)

    keep_raw = raw.get("keep", [])
    remove_raw = raw.get("remove", [])
    valid_range = set(range(len(words)))

    # Keep only valid integer indices in range
    keep_valid = [i for i in keep_raw if isinstance(i, int) and i in valid_range]
    remove_valid = [i for i in remove_raw if isinstance(i, int) and i in valid_range]

    # Track duplicates inside each list
    keep_seen = set()
    keep_dupes = set()
    keep_unique = []
    for i in keep_valid:
        if i in keep_seen:
            keep_dupes.add(i)
        else:
            keep_seen.add(i)
            keep_unique.append(i)

    remove_seen = set()
    remove_dupes = set()
    remove_unique = []
    for i in remove_valid:
        if i in remove_seen:
            remove_dupes.add(i)
        else:
            remove_seen.add(i)
            remove_unique.append(i)

    # Track conflicts across both lists
    keep_set = set(keep_unique)
    remove_set = set(remove_unique)
    cross_dupes = keep_set & remove_set

    review = set()
    review.update(keep_dupes)
    review.update(remove_dupes)
    review.update(cross_dupes)

    # Remove reviewed indices from keep/remove
    keep_final = [i for i in keep_unique if i not in review]
    remove_final = [i for i in remove_unique if i not in review]

    assigned = set(keep_final) | set(remove_final)
    missing = valid_range - assigned
    review.update(missing)

    return {
        "keep": sorted(keep_final),
        "remove": sorted(remove_final),
        "review": sorted(review),
    }


def process_batch(batch_index: int, batch: List[str]) -> Dict[str, List[str]]:
    log(f"Processing batch {batch_index} ({len(batch)} words)...")

    try:
        result = classify_batch(batch)

        keep_words = indices_to_words(batch, result["keep"])
        remove_words = indices_to_words(batch, result["remove"])
        review_words = indices_to_words(batch, result["review"])

        log(
            f"Finished batch {batch_index}: "
            f"keep={len(keep_words)}, remove={len(remove_words)}, review={len(review_words)}"
        )

        return {
            "keep": keep_words,
            "remove": remove_words,
            "review": review_words,
        }

    except Exception as e:
        log(f"Batch {batch_index} failed: {e}")
        return {
            "keep": [],
            "remove": [],
            "review": batch,
        }


def main() -> None:
    all_words = read_words(INPUT_FILE)

    pre_removed = [w for w in all_words if obvious_remove(w)]
    remaining_words = [w for w in all_words if not obvious_remove(w)]

    log(f"Total unique words: {len(all_words)}")
    log(f"Pre-removed: {len(pre_removed)}")
    log(f"Remaining for AI: {len(remaining_words)}")

    batches = chunked(remaining_words, BATCH_SIZE)
    log(f"Total batches: {len(batches)}")
    log(f"Using MAX_WORKERS={MAX_WORKERS}")

    keep_all: List[str] = []
    remove_all: List[str] = list(pre_removed)
    review_all: List[str] = []

    with ThreadPoolExecutor(max_workers=MAX_WORKERS) as executor:
        futures = {
            executor.submit(process_batch, i, batch): i
            for i, batch in enumerate(batches, start=1)
        }

        for future in as_completed(futures):
            result = future.result()
            keep_all.extend(result["keep"])
            remove_all.extend(result["remove"])
            review_all.extend(result["review"])

    write_list(OUTPUT_KEEP, keep_all)
    write_list(OUTPUT_REMOVE, remove_all)
    write_list(OUTPUT_REVIEW, review_all)

    log("Done.")
    log(f"Kept:    {len(keep_all)}")
    log(f"Removed: {len(remove_all)}")
    log(f"Review:  {len(review_all)}")


if __name__ == "__main__":
    main()