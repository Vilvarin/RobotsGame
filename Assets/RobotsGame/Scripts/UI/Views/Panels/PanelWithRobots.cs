using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Панель, содержащая иконки роботов.
    /// </summary>
    public class PanelWithRobots : MonoBehaviour
    {
        /// <summary>Прототип иконки</summary>
        public GameObject icon;

        protected Transform _transform;

        protected RectTransform _drillers;
        protected int _drillersCount;

        protected RectTransform _collectors;
        protected int _collectorsCount;

        protected RectTransform _dozers;
        protected int _dozersCount;

        protected RectTransform _bombers;
        protected int _bombersCount;

        protected float _iconWidth;
        protected float _iconHeight;

        protected void Start()
        {
            _transform  = GetComponent<Transform>();
            _iconWidth  = icon.GetComponent<RectTransform>().sizeDelta.x;
            _iconHeight = icon.GetComponent<RectTransform>().sizeDelta.y;

            _drillers = _transform.FindChild("Drillers").GetComponent<RectTransform>();
            _collectors = _transform.FindChild("Collectors").GetComponent<RectTransform>();
            _dozers = _transform.FindChild("Dozers").GetComponent<RectTransform>();
            _bombers = _transform.FindChild("Bombers").GetComponent<RectTransform>();
        }

        /// <summary>
        /// Добавить иконку робота на панель
        /// </summary>
        /// <param name="robot">Ссылка на робота, к которому закреплена кнопка</param>
        public virtual RectTransform AddRobot(Robot robot)
        {
            RectTransform instance = Instantiate(icon).GetComponent<RectTransform>();
            Image img = instance.GetComponent<Image>();

            img.sprite = robot.Shassis.ShassisSprite;

            switch (robot.Instrument.InstrumentCode)
            {
                case InstrumentsEnum.None:
                    throw new MissingComponentException("Неопределнное шасси робота");

                case InstrumentsEnum.Bomb:
                    instance.SetParent(_bombers);
                    instance.anchoredPosition = new Vector2(- _bombersCount * _iconWidth - _iconWidth / 2, 0);
                    _bombersCount++;
                    break;

                case InstrumentsEnum.Collect:
                    instance.SetParent(_collectors);
                    instance.anchoredPosition = new Vector2(- _collectorsCount * _iconWidth - _iconWidth / 2, 0);
                    _collectorsCount++;
                    break;

                case InstrumentsEnum.Drill:
                    instance.SetParent(_drillers);
                    instance.anchoredPosition = new Vector2(- _drillersCount * _iconWidth - _iconWidth / 2, 0);
                    _drillersCount++;
                    break;

                case InstrumentsEnum.Dozer:
                    instance.SetParent(_dozers);
                    instance.anchoredPosition = new Vector2(- _dozersCount * _iconWidth - _iconWidth / 2, 0);
                    _dozersCount++;
                    break;

                default:
                    throw new MissingComponentException("Передан незарегистрированный инструмент робота");
            }

            return instance;
        }
    }
}

