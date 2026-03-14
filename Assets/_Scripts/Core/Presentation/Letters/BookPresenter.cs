using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.GameConfig;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class BookPresenter : MonoBehaviour
    {
        [SerializeField] private LetterPresenter _letterPresenterPrefab;
        [SerializeField] private Transform _leftPageLetterContainer;
        [SerializeField] private Transform _rightPageLetterContainer;

        private Transform[] _letterPlaceholders;
        private LetterGenerator _letterGenerator;

        [Inject]
        public void Inject(LetterGenerator letterGenerator, GameConfiguration configuration)
        {
            _letterGenerator = letterGenerator;
            _letterPlaceholders = new Transform[configuration.LettersInBook];
            GeneratePlaceholders();
        }

        private void GeneratePlaceholders()
        {
            var maxPlaceholdersInLeftPage = _letterPlaceholders.Length / 2;

            for (int i = 0; i < _letterPlaceholders.Length; i++)
            {
                Transform placeholder;
                if (i < maxPlaceholdersInLeftPage)
                    placeholder = Instantiate(_letterPresenterPrefab, _leftPageLetterContainer).transform;
                else
                    placeholder = Instantiate(_letterPresenterPrefab, _rightPageLetterContainer).transform;
                
                _letterPlaceholders[i] = placeholder;
            }
        }

        private void OnEnable()
        {
            _letterGenerator.OnPageFilled += HandlePageFilled;
        }

        private void OnDisable()
        {
            _letterGenerator.OnPageFilled -= HandlePageFilled;
        }

        private void HandlePageFilled(object _, LetterPage letterPage)
        {
            
        }
    }
}
