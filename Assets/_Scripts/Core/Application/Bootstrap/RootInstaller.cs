using Reflex.Core;
using Reflex.Enums;
using System;
using UnityEngine;
using WizardsSpellbook.Core.Domain.GameConfig;
using WizardsSpellbook.Core.Domain.Words;
using Resolution = Reflex.Enums.Resolution;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class RootInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameConfigurationData _configuration;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterFactory(container => new GameConfiguration(_configuration), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterFactory(container =>
            {
                var textAsset = Resources.Load<TextAsset>("words_clean");
                var words = textAsset.text.Split(Environment.NewLine);
                return new WordDictionary(words);
            }, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}