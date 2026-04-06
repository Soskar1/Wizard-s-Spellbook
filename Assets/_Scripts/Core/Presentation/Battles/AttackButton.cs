using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using WizardsSpellbook.Core.Application.Battles;

namespace WizardsSpellbook.Core.Presentation.Battles
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        private PlayerTurnProcessor _playerTurnProcessor;

        [Inject]
        public void Inject(PlayerTurnProcessor playerTurnProcessor)
        {
            _playerTurnProcessor = playerTurnProcessor;
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
            _attackButton.interactable = false;
            _playerTurnProcessor.Attack();
        }

        public void SetActive(bool isActive) => _attackButton.interactable = isActive;
    }
}
