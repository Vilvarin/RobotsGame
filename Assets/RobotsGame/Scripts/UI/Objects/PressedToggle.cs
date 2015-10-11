using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Скрипт для правильного изменения цвета переключателя 
    /// в окне с несколькими группами переключателей.
    /// </summary>
    public class PressedToggle : MonoBehaviour
    {
        /// <summary>Цвет переключателя в нажатом состоянии</summary>
        public Color onColor = new Color(20, 100, 50);
        /// <summary>Цвет переключателя в отпущенном состоянии</summary>
        public Color offColor = Color.white;

        private Toggle _toggle;
        private Image _image;

        void Start()
        {
            _toggle = GetComponent<Toggle>();
            _image = GetComponent<Image>();

            _toggle.onValueChanged.AddListener(delegate
            {
                if (_image.color == offColor)
                    _image.color = onColor;
                else
                    _image.color = offColor;
            });
        }
    }
}

