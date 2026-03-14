using UnityEngine;

namespace WizardsSpellbook.Core.Domain.GameConfig
{
    [CreateAssetMenu(menuName = "Wizards Spell book/Game Configuration")]
    public class GameConfigurationData : ScriptableObject
    {
        [SerializeField] private int _lettersInBook = 50;

        public int LettersInBook => _lettersInBook;
    }
}
