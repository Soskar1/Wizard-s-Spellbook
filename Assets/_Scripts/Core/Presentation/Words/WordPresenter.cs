using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using WizardsSpellbook.Core.Domain.Words;
using WizardsSpellbook.Core.Presentation.Letters;

namespace WizardsSpellbook.Core.Presentation.Words
{
    public class WordPresenter : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;

        private Word _word;
        private LetterPool _letterPool;

        [Inject]
        public void Inject(Word word, LetterPool letterPool)
        {
            _word = word;
            _letterPool = letterPool;
        }

        private void OnEnable()
        {
            _word.WordChanged += HandleWordChanged;
        }

        private void OnDisable()
        {
            _word.WordChanged -= HandleWordChanged;
        }

        private void HandleWordChanged(object _, WordChangedEventArgs args)
        {
            if (args.OperationType == WordOperationType.AddedLetter)
            {
                var presenter = _letterPool.GetPresenter(args.Letter);
                presenter.transform.SetParent(transform);
            }

            _attackButton.interactable = args.WordIsValid;
        }
    }
}
