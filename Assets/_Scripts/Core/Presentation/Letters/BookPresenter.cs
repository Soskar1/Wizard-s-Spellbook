using System;
using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class BookPresenter : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _leftPageLetterContainer;
        [SerializeField] private Transform _rightPageLetterContainer;

        private LetterPresenterFactory _factory;
        private LetterPresenter[] _letterPresenters;
        private Book _book;

        private int _maxPresentersInLeftPage;

        [Inject]
        public void Inject(Book book, LetterPresenterFactory factory)
        {
            _book = book;
            _factory = factory;
            _letterPresenters = new LetterPresenter[_book.MaxSize];
            _maxPresentersInLeftPage = _book.MaxSize / 2;
        }

        public void OnEnable()
        {
            _book.OnLetterSetEventArgs += HandleOnLetterSetEventArgs;
        }

        public void OnDisable() => Dispose();

        private void HandleOnLetterSetEventArgs(object _, LetterSetEventArgs args)
        {
            var page = args.Index < _maxPresentersInLeftPage ? _leftPageLetterContainer : _rightPageLetterContainer;
            var presenter = _factory.Create(page, args.Letter);

            _letterPresenters[args.Index] = presenter;
        }

        public void Dispose()
        {
            _book.OnLetterSetEventArgs -= HandleOnLetterSetEventArgs;
        }
    }
}
