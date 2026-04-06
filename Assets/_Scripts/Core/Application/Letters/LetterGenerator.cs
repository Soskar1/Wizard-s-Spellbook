using System;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Letters
{
    public class LetterGenerator
    {
        private readonly AlphabetInventory _alphabetInventory;
        private readonly Random _random;

        public LetterGenerator(AlphabetInventory alphabetInventory, Random random)
        {
            _alphabetInventory = alphabetInventory;
            _random = random;
        }

        public Letter GenerateLetter()
        {
            var letterIndex = _random.Next(_alphabetInventory.Size);
            return _alphabetInventory.GetLetter(letterIndex);
        }
    }
}
