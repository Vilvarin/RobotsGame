using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Отображение спрайта выбранного робота
    /// </summary>
    public class PreviewImage : MonoBehaviour
    {
        /// <summary>Начальный цвет, чтобы картинка была невидима</summary>
        public Color startColor;
        /// <summary>Цвет при добавлении спрайта</summary>
        public Color spriteColor;

        Image _image;

        void Start()
        {
            _image = GetComponent<Image>();
            _image.color = startColor;
        }

        /// <summary>
        /// Показать переданный спрайт
        /// </summary>
        /// <param name="sprite">Спрайт для показа</param>
        public void DisplaySprite(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.color = spriteColor;
        }
    }
}

