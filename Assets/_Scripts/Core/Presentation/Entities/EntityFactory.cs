using UnityEngine;
using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class EntityFactory
    {
        private Entity _entityPrefab;
        
        public EntityFactory(Entity entityPrefab)
        {
            _entityPrefab = entityPrefab;
        }

        public Entity Create(EntityConfiguration configuration)
        {
            var entity = GameObject.Instantiate(_entityPrefab);
            entity.Initialize(configuration);
            
            return entity;
        }
    }
}
