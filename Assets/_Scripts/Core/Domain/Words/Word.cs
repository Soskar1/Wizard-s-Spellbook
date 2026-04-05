using System;
using System.Collections.Generic;
using System.Text;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class Word
    {
        private readonly List<Letter> _letters;
        private readonly StringBuilder _currentWordRepresentation;
        private readonly WordDictionary _wordDictionary;

        public event EventHandler<WordChangedEventArgs> WordChanged;

        public Word(WordDictionary wordDictionary)
        {
            _letters = new List<Letter>();
            _currentWordRepresentation = new StringBuilder();
            _wordDictionary = wordDictionary;
        }

        public void AddLetter(Letter letter)
        {
            _letters.Add(letter);
            _currentWordRepresentation.Append(letter.Character);

            var wordIsValid = ValidateWord();

            var args = new WordChangedEventArgs(WordOperationType.AddedLetter, letter, wordIsValid);
            WordChanged?.Invoke(this, args);
        }
        
        public void RemoveLetter(Letter letter)
        {
            var index = 0;

            for (; index < _letters.Count; ++index)
            {
                if (_letters[index] == letter)
                {
                    break;
                }
            }

            _letters.RemoveAt(index);
            _currentWordRepresentation.Remove(index, 1);

            var wordIsValid = ValidateWord();

            var args = new WordChangedEventArgs(WordOperationType.RemovedLetter, letter, wordIsValid);
            WordChanged?.Invoke(this, args);
        }

        private bool ValidateWord() => _wordDictionary.FindWord(_currentWordRepresentation.ToString());

        public bool Contains(Letter letter) => _letters.Contains(letter);
    }
}
