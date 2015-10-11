using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Кнопка включения инструмента
    /// </summary>
    public class InstrumentButton : MonoBehaviour
    {
        public Color onColor;
        public Color offColor;
        public string onText = "Выключить инструмент";
        public string offText = "Включить инструмент";

        Button _button;
        Text _text;

        public event EventHandler ClickAction = delegate { };

        void Awake()
        {
            _button = GetComponent<Button>();
            _text = transform.FindChild("Text").GetComponent<Text>();

            _button.onClick.AddListener(delegate
            {
                ClickAction(this, EventArgs.Empty);
            });
        }

        void Start()
        {
            _text.text = offText;
            _button.image.color = offColor;
        }

        /// <summary>
        /// Перевести кнопку в состояние включенного инструмента или выключенного
        /// </summary>
        /// <param name="state">true если кнопка должна включать инструмент</param>
        public void ChangeState(bool state)
        {
            if (state)
            {
                _text.text = onText;
                _button.image.color = onColor;
            }
            else
            {
                _text.text = offText;
                _button.image.color = offColor;
            }
        }
    }
}

