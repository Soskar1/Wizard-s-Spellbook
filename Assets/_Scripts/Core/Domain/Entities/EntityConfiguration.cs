using UnityEngine;

namespace WizardsSpellbook.Core.Domain.Entities
{
    [CreateAssetMenu(menuName = "Wizard's Spellbook/EntityModel Data")]
    public class EntityConfiguration : ScriptableObject
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private AnimatorOverrideController _animatorController;

        public int StartHealth => _startHealth;
        public AnimatorOverrideController AnimatorController => _animatorController;
    }
}
