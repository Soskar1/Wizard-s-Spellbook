using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class LetterInventory
    {
        private readonly IReadOnlyList<WeightedLetter> _inventory;
        private readonly Random _random;
        private readonly int _totalWeight;

        public LetterInventory(LetterConfiguration configuration, Random random)
        {
            _inventory = configuration.Letters;
            _random = random;

            _totalWeight = _inventory.Sum(weightedLetter => weightedLetter.Weight);
        }

        public Letter GetLetter()
        {
            int weight = _random.Next(_totalWeight);

            int current = 0;
            foreach (var item in _inventory)
            {
                current += item.Weight;
                if (weight < current)
                {
                    return new Letter(item.Letter);
                }
            }

            throw new InvalidOperationException("No letters in inventory.");
        }
    }
}
