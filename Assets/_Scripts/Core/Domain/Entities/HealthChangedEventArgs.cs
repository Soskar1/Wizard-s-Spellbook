using System;

namespace WizardsSpellbook.Core.Domain.Entities
{
    public class HealthChangedEventArgs : EventArgs
    {
        public int Health { get; }

        public HealthChangedEventArgs(int health)
        {
            Health = health; 
        }
    }
}
