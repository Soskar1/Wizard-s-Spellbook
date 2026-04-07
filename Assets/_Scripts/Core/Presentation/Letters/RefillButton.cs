using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using WizardsSpellbook.Core.Application.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class RefillButton : MonoBehaviour
    {
        [SerializeField] private Button _refill;

        private BookFill _bookFill; 

        [Inject]
        public void Inject(BookFill bookFill)
        {
            _bookFill = bookFill;
        }

        public void OnEnable()
        {
            _refill.onClick.AddListener(Refill);
        }

        public void OnDisable()
        {
            _refill.onClick.RemoveAllListeners();
        }

        private void Refill()
        {
            _bookFill.Refill();
        }
    }
}
