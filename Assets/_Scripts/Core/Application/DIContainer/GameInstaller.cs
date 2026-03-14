using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.GameConfig;
using WizardsSpellbook.Core.Domain.Letters;
using Resolution = Reflex.Enums.Resolution;

namespace WizardsSpellbook.Core.Application.DIContainer
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameConfigurationData _configuration;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterFactory(container => new GameConfiguration(_configuration), Lifetime.Singleton, Resolution.Eager);
            containerBuilder.RegisterFactory(container => new System.Random(), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(AlphabetInventory), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LetterPage), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LetterGenerator), Lifetime.Singleton, Resolution.Lazy);
        }
    }
}
