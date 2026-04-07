using System;
using System.Collections.Generic;
using WizardsSpellbook.Core.Domain.GameConfig;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class Book
    {
        private readonly Letter[] _letters;
        public int MaxSize => _letters.Length;

        public event EventHandler<BookChangedEventArgs> BookChanged;

        public Book(GameConfiguration configuration)
        {
            _letters = new Letter[configuration.LettersInBook];
        }

        public Letter GetLetter(int index) => _letters[index];
        public void SetLetter(int index, Letter letter)
        {
            if (_letters[index] != null)
            {
                throw new InvalidOperationException("Letter slot already occupied.");
            }

            _letters[index] = letter;

            var args = new BookChangedEventArgs(new int[] { index }, new Letter[] { letter }, BookOperationType.Set);
            BookChanged?.Invoke(this, args);
        }
        
        public int RemoveLetter(Letter letterToRemove)
        {
            var index = 0;

            for (; index < _letters.Length; ++index)
            {
                if (letterToRemove == _letters[index])
                {
                    break;
                }
            }

            _letters[index] = null;
            return index;
        }

        public void Clear()
        {
            var indexes = new List<int>(_letters.Length);
            var letters = new List<Letter>(_letters.Length);

            for (var index = 0; index < _letters.Length; ++index)
            {
                var letter = _letters[index];

                if (letter == null)
                {
                    continue;
                }

                indexes.Add(index);
                letters.Add(letter);
                _letters[index] = null;
            }

            var args = new BookChangedEventArgs(indexes.ToArray(), letters.ToArray(), BookOperationType.Clear);
            BookChanged?.Invoke(this, args);
        }
    }
}
