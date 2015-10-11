using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Представление для кнопки добавления робота в конструкторе.
    /// </summary>
    /// <remarks>У кнопки есть два состояния. В разных состояниях вызываются разные события при клике.</remarks>
    public class ReedyButton : MonoBehaviour
    {
        /// <summary>Текст в состоянии AddRobot </summary>
        public string addRobotText = "Добавить робота";
        /// <summary>Текст в состоянии ClosePanel </summary>
        public string closePanelText = "Начать игру";

        /// <summary>Событие вызывается при клике в состоянии AddRobot</summary>
        public event EventHandler AddRobotAction = delegate { };
        /// <summary>Событие вызывается при клике в состоянии ClosePanel</summary>
        public event EventHandler ClosePanelAction = delegate { };

        Text _text;
        Button _button;

        void Start()
        {
            _button = GetComponent<Button>();
            _text = transform.FindChild("Text").GetComponent<Text>();
            _text.text = addRobotText;

            _button.onClick.AddListener(OnAddRobot);
        }

        /// <summary>
        /// Смена состояния кнопки
        /// </summary>
        public void SwitchState()
        {
            _text.text = closePanelText;
            _button.onClick.RemoveListener(OnAddRobot);
            _button.onClick.AddListener(OnClosePanel);
        }
        
        void OnAddRobot()
        {
            AddRobotAction(this, EventArgs.Empty);
        }

        void OnClosePanel()
        {
            ClosePanelAction(this, EventArgs.Empty);
        }
    }
}

