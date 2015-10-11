using System;
using UnityEngine;
using UnityEngine.UI;

namespace RobotsGame.UI
{
    /// <summary>
    /// Кнопка выбора активного робота
    /// </summary>
    public class RobotButton : MonoBehaviour
    {
        public event EventHandler<GenericEventArgs<Robot>> ClickAction = delegate { };

        Robot _robot;
        GenericEventArgs<Robot> _robotArgs;
        Button _button;
        
        /// <summary>
        /// Получить ссылку на установленного на кнопку робота
        /// </summary>
        public Robot FixedRobot
        {
            get { return _robot; }
        }

        void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(delegate
            {
                ClickAction(this, _robotArgs);
            });
        }

        /// <summary>
        /// Установить робота на кнопку
        /// </summary>
        /// <param name="robot">Ссылка на робота, который должен стать активным при нажатии на кнопку</param>
        public void FixRobot(Robot robot)
        {
            _robot = robot;
            _robotArgs = new GenericEventArgs<Robot>(robot);
        }

        /// <summary>
        /// На кнопку можно будет нажать
        /// </summary>
        public void Activate()
        {
            _button.interactable = true;
        }

        /// <summary>
        /// На кнопку нельзя нажать
        /// </summary>
        public void Deactivate()
        {
            _button.interactable = false;
        }
    }
}
