using UnityEngine;
using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public EntityModel Model { get; private set; }

        public void Initialize(EntityConfiguration configuration)
        {
            Model = new EntityModel(configuration);

            _animator.runtimeAnimatorController = configuration.AnimatorController;
        }
    }
}