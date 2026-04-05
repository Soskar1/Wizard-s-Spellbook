using UnityEngine;

namespace WizardsSpellbook.Core.Domain.Entities
{
    [CreateAssetMenu(menuName = "Wizard's Spellbook/Entity Data")]
    public class EntityData : ScriptableObject
    {
        [SerializeField] private int _startHealth;

        public int StartHealth => _startHealth;
    }
}
