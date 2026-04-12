using UnityEngine;
using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class EntityPresenter : MonoBehaviour
    {
        [SerializeField] private HealthPresenter _healthPresenter;

        public void Initialize(EntityModel entity)
        {
            _healthPresenter.Initialize(entity.Health);
        }
    }
}
