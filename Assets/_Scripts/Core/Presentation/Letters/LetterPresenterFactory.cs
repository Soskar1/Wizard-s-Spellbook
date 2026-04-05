using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class LetterPresenterFactory
    {
        private readonly Container _container;
        private readonly LetterPresenter _prefab;

        public LetterPresenterFactory(Container container, LetterPresenter prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public LetterPresenter Create(Letter letter)
        {
            var presenter = Object.Instantiate(_prefab);
            GameObjectInjector.InjectRecursive(presenter.gameObject, _container);

            presenter.Initialize(letter);

            return presenter;
        }
    }
}
