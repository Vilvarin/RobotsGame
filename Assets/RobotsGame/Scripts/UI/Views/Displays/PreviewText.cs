using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Отображение для текста названия робота
    /// </summary>
    /// <remarks>Текст составляется из названия шасси и инструмента, которые хранятся в приватных полях класса.</remarks>
    public class PreviewText : MonoBehaviour 
    {
        Text _text;
        string _instrument = "";
        string _shassis = "";

        void Start()
        {
            _text = GetComponent<Text>();
        }

        /// <summary>
        /// Вывести название инструмента
        /// </summary>
        /// <param name="instrument">инструмент</param>
        public void DisplayInstrument(string instrument)
        {
            _instrument = instrument;
            UpdateText();
        }

        /// <summary>
        /// Вывести название шасси
        /// </summary>
        /// <param name="shassis">шасси</param>
        public void DisplayShassis(string shassis)
        {
            _shassis = shassis;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = _shassis + " " + _instrument;
        }
    }
}

