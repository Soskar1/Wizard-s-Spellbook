using System.Threading.Tasks;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public class AiTurnProcessor : ITurnProcessor
    {
        public Task<TurnResult> StartTurn()
        {
            var turnResult = new TurnResult(1, BattleSide.Ai);
            return Task.FromResult<TurnResult>(turnResult);
        }
    }
}