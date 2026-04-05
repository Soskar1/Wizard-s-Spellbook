using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace WizardsSpellbook.Core.Presentation.Actions
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;

        [Inject]
        public void Inject()
        {
        }

        public void Awake()
        {
            _attackButton.onClick.AddListener(Attack);
        }

        public void OnDestroy()
        {
            _attackButton.onClick.RemoveAllListeners();
        }

        private void Attack()
        {
        }
    }
}
