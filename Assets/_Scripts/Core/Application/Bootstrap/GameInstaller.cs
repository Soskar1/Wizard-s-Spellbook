using System.Collections.Generic;
using System.Linq;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using WizardsSpellbook.Core.Application.Battles;
using WizardsSpellbook.Core.Application.Entities;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.GameConfig;
using WizardsSpellbook.Core.Domain.Letters;
using WizardsSpellbook.Core.Domain.Words;
using WizardsSpellbook.Core.Presentation.Letters;
using Resolution = Reflex.Enums.Resolution;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameConfigurationData _configuration;
        [SerializeField] private LetterPresenter _letterPresenterPrefab;
        [SerializeField] private List<string> _validWords;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterFactory(container => new GameConfiguration(_configuration), Lifetime.Singleton, Resolution.Eager);
            containerBuilder.RegisterFactory(container => new System.Random(), Lifetime.Singleton, Resolution.Lazy);
            
            containerBuilder.RegisterFactory(container => new LetterPresenterFactory(container, _letterPresenterPrefab), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LetterPool), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(AlphabetInventory), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(Book), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LetterGenerator), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(BookFill), Lifetime.Singleton, Resolution.Lazy);
            
            containerBuilder.RegisterFactory(container => new WordDictionary(_validWords.ToHashSet()), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(Word), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(WordBuilder), Lifetime.Singleton, Resolution.Lazy);

            containerBuilder.RegisterType(typeof(EntityFactory), Lifetime.Singleton, Resolution.Lazy);

            containerBuilder.RegisterType(typeof(AiTurnProcessor), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(PlayerTurnProcessor), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(BattleProcessor), Lifetime.Singleton, Resolution.Lazy);
        }
    }
}
