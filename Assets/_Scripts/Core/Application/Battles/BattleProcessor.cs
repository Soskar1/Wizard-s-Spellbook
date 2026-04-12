using System.Threading.Tasks;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public class BattleProcessor
    {
        private readonly PlayerTurnProcessor _playerTurnProcessor;
        private readonly AiTurnProcessor _aiTurnProcessor;
        private ITurnProcessor _currentTurnProcessor;
        private BattleSide _currentBattleSide;

        public BattleProcessor(PlayerTurnProcessor playerTurnProcessor, AiTurnProcessor aiTurnProcessor)
        {
            _playerTurnProcessor = playerTurnProcessor;
            _aiTurnProcessor = aiTurnProcessor;
        }

        public async Task<BattleResult> StartBattle(Battle battle)
        {
            _currentBattleSide = battle.Start();
            _currentTurnProcessor = GetTurnProcessor(_currentBattleSide);

            var battleResult = await ProcessBattle(battle);
            return battleResult;
        }

        private async Task<BattleResult> ProcessBattle(Battle battle)
        {
            var battleIsEnded = false;
            
            while (!battleIsEnded)
            {
                var turnResult = await _currentTurnProcessor.StartTurn();
                battleIsEnded = battle.ProcessTurnResult(turnResult);

                if (battleIsEnded)
                {
                    break;
                }

                _currentBattleSide = battle.NextTurn();
                _currentTurnProcessor = GetTurnProcessor(_currentBattleSide);
            }

            return new BattleResult(_currentBattleSide);
        }

        private ITurnProcessor GetTurnProcessor(BattleSide battleSide)
        {
            if (battleSide == BattleSide.Player)
            {
                return _playerTurnProcessor;
            }

            return _aiTurnProcessor;
        }
    }
}