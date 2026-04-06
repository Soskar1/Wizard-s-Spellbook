using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Domain.Battles
{
    public class Battle
    {
        private Entity _player;
        private Entity _enemy;
        private BattleSide _currentTurn;

        public Battle(Entity player, Entity enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public BattleSide Start()
        {
            _currentTurn = BattleSide.Player;
            return _currentTurn;
        }

        public BattleSide NextTurn()
        {
            _currentTurn = _currentTurn == BattleSide.Player ? BattleSide.Ai : BattleSide.Player;
            return _currentTurn;
        }

        public bool ProcessTurnResult(TurnResult turnResult)
        {
            var battleIsEnded = false;
            var damage = turnResult.Damage;
            int remainingHealth;

            if (turnResult.BattleSide == BattleSide.Player)
            {
                remainingHealth = _enemy.TakeDamage(damage);
            }
            else
            {
                remainingHealth = _player.TakeDamage(damage);
            }

            if (remainingHealth <= 0)
            {
                battleIsEnded = true;
            }

            return battleIsEnded;
        }
    }
}