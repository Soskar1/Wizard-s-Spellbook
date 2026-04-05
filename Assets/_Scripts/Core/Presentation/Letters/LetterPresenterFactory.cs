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
        private readonly LetterPool _letterPool;

        public LetterPresenterFactory(Container container, LetterPresenter prefab, LetterPool letterPool)
        {
            _container = container;
            _prefab = prefab;
            _letterPool = letterPool;
        }

        public LetterPresenter Create(Transform parent, Letter letter)
        {
            var presenter = Object.Instantiate(_prefab, parent);
            GameObjectInjector.InjectRecursive(presenter.gameObject, _container);

            presenter.Initialize(letter);
            _letterPool.Add(letter, presenter);

            return presenter;
        }
    }
}
