using System;
using WizardsSpellbook.Core.Domain.GameConfig;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class Book
    {
        private readonly Letter[] _letters;
        public int MaxSize => _letters.Length;

        public event EventHandler<LetterSetEventArgs> OnLetterSetEventArgs;

        public Book(GameConfiguration configuration)
        {
            _letters = new Letter[configuration.LettersInBook];
        }

        public Letter GetLetter(int index) => _letters[index];
        public void SetLetter(int index, Letter letter)
        {
            if (_letters[index] != null)
                throw new InvalidOperationException("Letter slot already occupied.");

            _letters[index] = letter;

            var args = new LetterSetEventArgs(index, letter);
            OnLetterSetEventArgs?.Invoke(this, args);
        }
    }
}
