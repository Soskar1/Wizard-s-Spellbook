using System.Collections.Generic;
using WizardsSpellbook.Core.Domain.Letters;
using WizardsSpellbook.Core.Domain.Words;

namespace WizardsSpellbook.Core.Application.Words
{
    public class WordBuilder
    {
        private readonly Word _word;
        private readonly Book _book;

        private readonly Dictionary<Letter, int> _removedLetterPositions;

        public WordBuilder(Word word, Book book)
        {
            _word = word;
            _book = book;
            _removedLetterPositions = new Dictionary<Letter, int>();
        }

        public void Clear()
        {
            _removedLetterPositions.Clear();
        }

        public void MoveLetter(Letter letter)
        {
            if (_word.Contains(letter))
            {
                MoveToBook(letter);
            }
            else
            {
                MoveToWord(letter);
            }
        }

        private void MoveToWord(Letter letter)
        {
            var index = _book.RemoveLetter(letter);
            _removedLetterPositions.Add(letter, index);

            _word.AddLetter(letter);
        }

        private void MoveToBook(Letter letter)
        {
            var index = _removedLetterPositions[letter];
            _removedLetterPositions.Remove(letter);

            _word.RemoveLetter(letter);
            _book.SetLetter(index, letter);
        }
    }
}
