using UnityEngine;

namespace WizardsSpellbook.Core.Domain.Letters
{
    [System.Serializable]
    public struct WeightedLetter
    {
        [SerializeField] private char _letter;
        [SerializeField] private int _weight;
        
        public char Letter => _letter;
        public int Weight => _weight;
    }
}
