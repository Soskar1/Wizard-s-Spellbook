using System.Collections.Specialized;
using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Letters;
using WizardsSpellbook.Core.Domain.Words;

namespace WizardsSpellbook.Core.Presentation.Words
{
    public class WordPresenter : MonoBehaviour
    {
        private Word _word;

        [Inject]
        public void Inject(Word word)
        {
            _word = word;
        }

        private void OnEnable()
        {
            _word.CollectionChanged += HandleCollectionChanged;
        }

        private void OnDisable()
        {
            _word.CollectionChanged -= HandleCollectionChanged;
        }

        private void HandleCollectionChanged(object _, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                var addedLetter = (Letter)args.NewItems[0];
                Debug.Log($"{addedLetter.Character} has been added!");
            }
        }
    }
}
