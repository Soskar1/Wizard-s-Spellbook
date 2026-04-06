using System.Threading.Tasks;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public class PlayerTurnProcessor : ITurnProcessor
    {
        private readonly WordBuilder _wordBuilder;
        private readonly BookFill _bookFill;
        private TaskCompletionSource<TurnResult> _turnFinished;

        public PlayerTurnProcessor(WordBuilder wordBuilder, BookFill bookFill)
        {
            _wordBuilder = wordBuilder;
            _bookFill = bookFill;
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

            _wordBuilder.Clear();
            _bookFill.Fill();
        }
    }
}