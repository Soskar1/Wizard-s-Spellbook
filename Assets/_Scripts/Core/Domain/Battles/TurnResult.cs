namespace WizardsSpellbook.Core.Domain.Battles
{
    public struct TurnResult
    {
        public int Damage { get; }
        public BattleSide BattleSide { get; }

        public TurnResult(int damage, BattleSide battleSide)
        {
            Damage = damage;
            BattleSide = battleSide;
        }
    }
}