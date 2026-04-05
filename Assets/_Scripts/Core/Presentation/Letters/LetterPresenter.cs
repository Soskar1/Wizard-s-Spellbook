using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class LetterPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;
        private Letter _letter;
        private WordBuilder _wordBuilder;

        [Inject]
        public void Inject(WordBuilder wordBuilder)
        {
            _wordBuilder = wordBuilder;
        }

        public void Initialize(Letter letter)
        {
            _letter = letter;
            _text.text = _letter.Character.ToString();
            _button.onClick.AddListener(HandleLetterClick);
        }

        private void HandleLetterClick()
        {
            _wordBuilder.MoveLetter(_letter);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
