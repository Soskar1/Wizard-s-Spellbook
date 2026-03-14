using System;
using WizardsSpellbook.Core.Domain.GameConfig;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class LetterPage
    {
        private readonly Letter[] _letters;
        public int PageSize { get; private set; }

        public LetterPage(GameConfiguration configuration)
        {
            _letters = new Letter[configuration.LettersInBook];
            PageSize = configuration.LettersInBook;
        }

        public Letter GetLetter(int index) => _letters[index];
        public void SetLetter(int index, Letter letter)
        {
            if (index < 0 || index >= _letters.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_letters[index] != null)
                throw new InvalidOperationException("Letter slot already occupied.");

            _letters[index] = letter;
        }
    }
}
