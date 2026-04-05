namespace WizardsSpellbook.Core.Domain.Entities
{
    public class Entity
    {
        public Health Health { get; }

        public Entity(EntityData entityData)
        {
            Health = new Health(entityData.StartHealth);
        }

        public void TakeDamage(int damage)
        {
            Health.Reduce(damage);
        }
    }
}
