using UnityEngine;
using WizardsSpellbook.Core.Domain.Entities;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private HeartContainerPresenter _heartContainerPrefab;

        private Health _health;
        private HeartContainerPresenter[] _heartContainers;

        private const int _healthPerContainer = 2;

        public void Initialize(Health health)
        {
            _health = health;

            var heartContainerCount = _health.CurrentHealth / _healthPerContainer;
            _heartContainers = new HeartContainerPresenter[heartContainerCount];

            for (var index = 0; index < heartContainerCount; ++index)
            {
                var heartContainer = Instantiate(_heartContainerPrefab, transform);
                _heartContainers[index] = heartContainer;
            }

            _health.HealthChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(object _, HealthChangedEventArgs args)
        {
            var fullContainers = args.Health / _healthPerContainer;

            for (var index = 0; index < fullContainers; ++index)
            {
                _heartContainers[index].SetFullHeart();
            }

            for (var index = fullContainers; index < _heartContainers.Length; ++index)
            {
                _heartContainers[index].SetEmptyHeart();
            }

            if (args.Health % _healthPerContainer != 0)
            {
                _heartContainers[fullContainers].SetHalfHeart();
            }
        }
    }
}
