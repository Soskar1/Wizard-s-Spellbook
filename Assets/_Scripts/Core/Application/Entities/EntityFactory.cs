using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Application.Entities
{
    public class EntityFactory
    {
        public Entity Create(EntityData data)
        {
            return new Entity(data);
        }
    }
}
