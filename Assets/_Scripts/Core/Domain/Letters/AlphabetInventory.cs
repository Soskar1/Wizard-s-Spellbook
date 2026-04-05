using System.Collections.Generic;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class AlphabetInventory
    {
        private readonly IList<char> _inventory;
        public int Size => _inventory.Count;

        public AlphabetInventory()
        {
            _inventory = new List<char>();
        }

        public void AddLetter(char letter) => _inventory.Add(letter);
        public Letter GetLetter(int index) => new Letter(_inventory[index]);
    }
}
