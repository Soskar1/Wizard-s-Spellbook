using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public struct BattleResult
    {
        public BattleSide Winner { get; private set; }

        public BattleResult(BattleSide winner) => Winner = winner;
    }
}