using TMPro;
using UnityEngine;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class LetterPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private Letter _letter;

        public void Initialize(Letter letter)
        {
            _letter = letter;
            _text.text = _letter.Character.ToString();
        }
    }
}
