using System;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Letters
{
    public class LetterGenerator
    {
        private readonly AlphabetInventory _alphabetInventory;
        private readonly Random _random;

        public event EventHandler<LetterPage> OnPageFilled;

        public LetterGenerator(AlphabetInventory alphabetInventory, Random random)
        {
            _alphabetInventory = alphabetInventory;
            _random = random;
        }

        public void FillPage(LetterPage letterPage)
        {
            for (var i = 0; i < letterPage.PageSize; ++i)
            {
                var letter = letterPage.GetLetter(i);

                if (letter == null)
                {
                    letter = GenerateLetter();
                    letterPage.SetLetter(i, letter);
                }
            }

            OnPageFilled?.Invoke(this, letterPage);
        }

        private Letter GenerateLetter()
        {
            var letterIndex = _random.Next(_alphabetInventory.Size);
            return _alphabetInventory.GetLetter(letterIndex);
        }
    }
}
