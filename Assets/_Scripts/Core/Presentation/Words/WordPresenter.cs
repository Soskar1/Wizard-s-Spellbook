using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Words;
using WizardsSpellbook.Core.Presentation.Battles;
using WizardsSpellbook.Core.Presentation.Letters;

namespace WizardsSpellbook.Core.Presentation.Words
{
    public class WordPresenter : MonoBehaviour
    {
        [SerializeField] private AttackButton _attackButton;

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
                var presenter = _letterPool.GetPresenter(args.Letters[0]);
                presenter.transform.SetParent(transform);
            }
            else if (args.OperationType == WordOperationType.Clear)
            {
                foreach (var letter in args.Letters)
                {
                    _letterPool.Remove(letter);
                }
            }

            _attackButton.SetActive(args.WordIsValid);
        }
    }
}
