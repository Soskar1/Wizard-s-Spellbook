using UnityEngine;
using UnityEngine.UI;

namespace WizardsSpellbook.Core.Presentation.Entities
{
    public class HeartContainerPresenter : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _halfHeart;
        [SerializeField] private Sprite _emptyHeart;

        public void SetFullHeart() => _image.sprite = _fullHeart;
        public void SetHalfHeart() => _image.sprite = _halfHeart;
        public void SetEmptyHeart() => _image.sprite = _emptyHeart;
    }
}
