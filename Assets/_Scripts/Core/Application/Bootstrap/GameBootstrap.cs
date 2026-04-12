using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Application.Battles;
using WizardsSpellbook.Core.Application.Entities;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.Battles;
using WizardsSpellbook.Core.Domain.Entities;
using WizardsSpellbook.Core.Presentation.Entities;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private EntityData _player;
        [SerializeField] private EntityData _enemy;
        [SerializeField] private EntityPresenter _playerPresenter;
        [SerializeField] private EntityPresenter _enemyPresenter;

        private EntityFactory _entityFactory;
        private BattleProcessor _battleProcessor;
        private BookFill _bookFill;

        [Inject]
        public void Inject(BookFill bookFill, EntityFactory entityFactory, BattleProcessor battleProcessor)
        {
            _bookFill = bookFill;
            _entityFactory = entityFactory;
            _battleProcessor = battleProcessor;
        }

        public async void Start()
        {
            var playerEntity = _entityFactory.Create(_player);
            _playerPresenter.Initialize(playerEntity);

            var enemyEntity = _entityFactory.Create(_enemy);
            _enemyPresenter.Initialize(enemyEntity);

            _bookFill.Fill();

            var battle = new Battle(playerEntity, enemyEntity);
            await _battleProcessor.StartBattle(battle);
        }
    }
}
