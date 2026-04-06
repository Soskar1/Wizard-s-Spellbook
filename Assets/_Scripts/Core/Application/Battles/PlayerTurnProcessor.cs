using System.Threading.Tasks;
using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public class PlayerTurnProcessor : ITurnProcessor
    {
        private readonly WordBuilder _wordBuilder;
        private TaskCompletionSource<TurnResult> _turnFinished;

        public PlayerTurnProcessor(WordBuilder wordBuilder)
        {
            _wordBuilder = wordBuilder;
        }

        public Task<TurnResult> StartTurn()
        {
            _turnFinished = new TaskCompletionSource<TurnResult>();
            return _turnFinished.Task;
        }

        public void Attack()
        {
            var turnResult = new TurnResult(1, BattleSide.Player);
            _turnFinished.SetResult(turnResult);
        }
    }
}