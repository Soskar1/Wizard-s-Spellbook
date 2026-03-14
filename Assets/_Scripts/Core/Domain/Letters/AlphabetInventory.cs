using System.Collections.Generic;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class AlphabetInventory
    {
        private readonly IList<Letter> _inventory;
        public int Size => _inventory.Count;

        public AlphabetInventory()
        {
            _inventory = new List<Letter>();
        }

        public void AddLetter(Letter letter) => _inventory.Add(letter);
        public Letter GetLetter(int index) => _inventory[index];
    }
}
