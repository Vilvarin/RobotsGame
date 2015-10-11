using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Компонент управления активным роботом
    /// </summary>
    public class RobotCommander : MonoBehaviour
    {
        #region fields and properties

        /// <summary>
        /// Спрайт для подсказки
        /// </summary>
        public GameObject robotSprite;
        /// <summary>
        /// Объект отрисовки линии для подсказки маршрута робота
        /// </summary>
        public LineRenderer lineRenderer;

        Transform _robotSprite_transform;
        SpriteRenderer _robotSprite_render;
        Robot _activeRobot;

        /// <summary>
        /// Устанавливает активного робота или возвращает ссылку на него
        /// </summary>
        public Robot ActiveRobot
        {
            get
            {
                if (_activeRobot != null)
                    return _activeRobot;

                throw new UnassignedReferenceException("Не установлен активный робот");
            }

            set
            {
                if (_activeRobot != null)
                {
                    _activeRobot.SwitchForwardStateAction -= _activeRobot_SwitchForwardStateAction;
                    _activeRobot.SwitchInstrumentStateAction -= _activeRobot_SwitchInstrumentStateAction;
                }
                

                _activeRobot = value;

                if (_activeRobot == null)
                    IsActive = false;
                else
                {
                    IsActive = true;
                    _activeRobot.SwitchForwardStateAction += _activeRobot_SwitchForwardStateAction;
                    _activeRobot.SwitchInstrumentStateAction += _activeRobot_SwitchInstrumentStateAction;
                }
            }
        }
        /// <summary>
        /// Вернёт true если установлен активный робот
        /// </summary>
        public bool IsActive { get; private set; }

        #endregion

        #region events

        /// <summary>Событие вызывается когда активный робот начал движение</summary>
        public event EventHandler<GenericEventArgs<Robot>> StartMoveAction = delegate { };
        /// <summary>Событие вызывается когда активный робот включит или выключит инструмент</summary>
        public event EventHandler<GenericEventArgs<bool>> SwitchInstrumentStateAction = delegate { };
        /// <summary>Событие вызывается когда активный робот начнёт возвращение на базу</summary>
        public event EventHandler<GenericEventArgs<bool>> SwitchForwardStateAction = delegate { };

        #endregion

        void Start()
        {
            robotSprite = Instantiate(robotSprite, Vector3.zero, Quaternion.identity) as GameObject;
            _robotSprite_transform = robotSprite.transform;
            _robotSprite_render = robotSprite.GetComponent<SpriteRenderer>();
        }

        #region public methods

        /// <summary>
        /// Отобразить спрайт робота на платформе и показать его маршрут
        /// </summary>
        /// <param name="platform">Платформа на которой возникает робот</param>
        public void EstablishRobotOnPlatform(Platform platform)
        {
            Vector3 position = platform.transform.position;

            robotSprite.SetActive(true);
            _robotSprite_transform.Translate(position + Vector3.forward);
            _robotSprite_render.sprite = ActiveRobot.Shassis.ShassisSprite;

            lineRenderer.gameObject.SetActive(true);
            ActiveRobot.Shassis.RenderLine(lineRenderer, position);
        }

        /// <summary>
        /// Снять спрайт робота с платформы и сделать неактивным
        /// </summary>
        public void RemoveRobotFromPlatform()
        {
            _robotSprite_transform.position = Vector3.zero;
            robotSprite.SetActive(false);
            lineRenderer.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Робот начнёт прохождение маршрута
        /// </summary>
        /// <param name="platform">Платформа с которой начнёт робот</param>
        public void StartRobotMove(Platform platform)
        {
            RemoveRobotFromPlatform();

            ActiveRobot.SetVisibility(true);

            ActiveRobot.transform.Translate(platform.transform.position);
            StartMoveAction(this, new GenericEventArgs<Robot>(_activeRobot));
            StartCoroutine(ActiveRobot.StartMove());

            GameManager.instance.Batterys--;
        }

        #endregion

        #region event delegates

        void _activeRobot_SwitchInstrumentStateAction(object sender, GenericEventArgs<bool> e)
        {
            SwitchInstrumentStateAction(this, e);
        }

        void _activeRobot_SwitchForwardStateAction(object sender, GenericEventArgs<bool> e)
        {
            SwitchForwardStateAction(this, e);
        }

        #endregion
    }
}

