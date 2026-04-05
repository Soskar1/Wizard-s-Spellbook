using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

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

        public LetterPresenter Create(Transform parent)
        {
            var presenter = Object.Instantiate(_prefab, parent);
            GameObjectInjector.InjectRecursive(presenter.gameObject, _container);
            return presenter;
        }
    }
}
