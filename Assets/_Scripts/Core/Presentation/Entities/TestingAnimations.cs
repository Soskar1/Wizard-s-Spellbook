using UnityEngine;
using WizardsSpellbook.UnityAnimationScripting;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class TestingAnimations : MonoBehaviour
    {
        [SerializeField] private AnimationPlayer _animationPlayer;

        [ContextMenu(itemName: "PlayAttackAnimation")]
        public void PlayAttackAnimation()
        {
            _animationPlayer.PlayAnimation(WizardStates.Base_Layer.AttackHash);
        }
    }
}