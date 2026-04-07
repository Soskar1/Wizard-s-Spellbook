using System.Collections.Generic;
using UnityEngine;

namespace WizardsSpellbook.Core.Domain.Letters
{
    [CreateAssetMenu(menuName = "Wizard's Spellbook/Game Configuration")]
    public class LetterConfiguration : ScriptableObject
    {
        [SerializeField] private int _lettersInBook = 50;
        [SerializeField] private List<WeightedLetter> _letters;

        public int LettersInBook => _lettersInBook;
        public IReadOnlyList<WeightedLetter> Letters => _letters;
    }
}
