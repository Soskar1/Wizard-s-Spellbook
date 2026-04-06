using System.Threading.Tasks;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public class BattleProcessor
    {
        private readonly PlayerTurnProcessor _playerTurnProcessor;
        private readonly AiTurnProcessor _aiTurnProcessor;
        private ITurnProcessor _currentTurnProcessor;

        public BattleProcessor(PlayerTurnProcessor playerTurnProcessor, AiTurnProcessor aiTurnProcessor)
        {
            _playerTurnProcessor = playerTurnProcessor;
            _aiTurnProcessor = aiTurnProcessor;
        }

        public async Task StartBattle(Battle battle)
        {
            var battleSide = battle.Start();
            _currentTurnProcessor = GetTurnProcessor(battleSide);

            await ProcessBattle(battle);
        }

        private async Task ProcessBattle(Battle battle)
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

                var battleSide = battle.NextTurn();
                _currentTurnProcessor = GetTurnProcessor(battleSide);
            }
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