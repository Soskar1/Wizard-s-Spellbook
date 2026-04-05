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

        private LetterPool _letterPool;
        private Transform[] _letterPlaceholders;
        private Book _book;

        [Inject]
        public void Inject(Book book, LetterPool letterPool)
        {
            _book = book;
            _letterPool = letterPool;
            _letterPlaceholders = new Transform[_book.MaxSize];
        }

        private void OnEnable() => _book.OnLetterSetEventArgs += HandleOnLetterSetEventArgs;

        private void Awake()
        {
            var maxPresentersInLeftPage = _book.MaxSize / 2;

            for (var index = 0; index < _book.MaxSize; ++index)
            {
                var placeholder = new GameObject($"Letter Placeholder {index}");
                placeholder.AddComponent<RectTransform>();

                var page = index < maxPresentersInLeftPage ? _leftPageLetterContainer : _rightPageLetterContainer;
                placeholder.transform.SetParent(page);

                _letterPlaceholders[index] = placeholder.transform;
            }
        }

        private void OnDisable() => Dispose();

        private void HandleOnLetterSetEventArgs(object _, LetterSetEventArgs args)
        {
            var placeholder = _letterPlaceholders[args.Index];
            var presenter = _letterPool.GetPresenter(args.Letter);
            presenter.transform.SetParent(placeholder, false);
            presenter.transform.position = placeholder.position;
        }

        public void Dispose() => _book.OnLetterSetEventArgs -= HandleOnLetterSetEventArgs;
    }
}
