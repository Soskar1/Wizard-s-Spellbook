using System;

namespace WizardsSpellbook.Core.Domain.Entities
{
    public class Health
    {
        public int CurrentHealth { get; private set; }

        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        public Health(int startHealth)
        {
            CurrentHealth = startHealth;
        }

        public int Reduce(int amount)
        {
            CurrentHealth -= amount;

            var args = new HealthChangedEventArgs(amount);
            HealthChanged?.Invoke(this, args);

            return CurrentHealth;
        }
    }
}
