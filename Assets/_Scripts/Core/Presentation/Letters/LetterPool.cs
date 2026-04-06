using System.Collections.Generic;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class LetterPool
    {
        private readonly Dictionary<Letter, LetterPresenter> _letterPool;
        private readonly LetterPresenterFactory _letterPresenterFactory;

        public LetterPool(LetterPresenterFactory factory)
        {
            _letterPool = new Dictionary<Letter, LetterPresenter>();
            _letterPresenterFactory = factory;
        }

        public void Remove(Letter letter)
        {
            if (_letterPool.TryGetValue(letter, out var presenter))
            {
                _letterPool.Remove(letter);
                GameObject.Destroy(presenter.gameObject);
            }
        }

        public LetterPresenter GetPresenter(Letter letter)
        {
            if (_letterPool.TryGetValue(letter, out var presenter))
            {
                return presenter;
            }

            presenter = _letterPresenterFactory.Create(letter);
            _letterPool.Add(letter, presenter);

            return presenter;
        }
    }
}
