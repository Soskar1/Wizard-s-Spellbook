namespace WizardsSpellbook.Core.Domain.Entities
{
    public class EntityModel
    {
        public Health Health { get; }

        public EntityModel(EntityConfiguration entityConfiguration)
        {
            Health = new Health(entityConfiguration.StartHealth);
        }

        public int TakeDamage(int damage)
        {
            return Health.Reduce(damage);
        }
    }
}
