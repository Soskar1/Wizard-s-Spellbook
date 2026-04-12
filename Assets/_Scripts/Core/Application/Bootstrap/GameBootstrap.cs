using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Application.Battles;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.Battles;
using WizardsSpellbook.Core.Domain.Entities;
using WizardsSpellbook.Core.Presentation.Entities;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private EntityConfiguration _player;
        [SerializeField] private EntityConfiguration _enemy;
        [SerializeField] private EntityPresenter _playerPresenter;
        [SerializeField] private EntityPresenter _enemyPresenter;

        [SerializeField] private Transform _playerSpawnpoint;
        [SerializeField] private Transform _enemySpawnpoint;

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
            playerEntity.transform.position = _playerSpawnpoint.position;
            _playerPresenter.Initialize(playerEntity.Model);

            var enemyEntity = _entityFactory.Create(_enemy);
            enemyEntity.transform.position = _enemySpawnpoint.position;
            _enemyPresenter.Initialize(enemyEntity.Model);

            _bookFill.Fill();

            var battle = new Battle(playerEntity.Model, enemyEntity.Model);
            await _battleProcessor.StartBattle(battle);
        }
    }
}
