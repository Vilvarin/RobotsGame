using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Базовый скрипт робота.
    /// </summary>
    /// <remarks>Содержит стратегии шасси и инструмента, обеспечивает связь между ними.</remarks>
    public class Robot : MonoBehaviour
    {
        /// <summary>Префаб для объекта мертвого робота</summary>
        public GameObject deadRobot;

        #region events
        /// <summary>Событие вызывается при остановке робота</summary>
        public event EventHandler<GenericEventArgs<Robot>> StoppageAction = delegate { };

        /// <summary>Событие вызывается при возврате робота на базу</summary>
        public event EventHandler<GenericEventArgs<Robot>> ReturnAction = delegate { };

        /// <summary>Событие вызывается при появлении робота в игре</summary>
        public event EventHandler<GenericEventArgs<Robot>> InvokeAction = delegate { };

        /// <summary>Событие вызывается при смене направления</summary>
        public event EventHandler<GenericEventArgs<bool>> SwitchForwardStateAction = delegate { };

        /// <summary>Событие вызывается при включениии-отключении инструмента</summary>
        public event EventHandler<GenericEventArgs<bool>> SwitchInstrumentStateAction = delegate { };

        #endregion

        #region private variables

        IShassis _shassis;
        IInstrument _instrument;
        Transform _transform;
        GenericEventArgs<Robot> _eventArgs;
        SpriteRenderer _render;
        BoxCollider2D _collider;

        bool _movingFlag = true;
        bool _forwardFlag = true;
        bool _usingInstrumentFlag = false;

        #endregion

        #region public properties

        /// <summary>
        /// Возвращает инструмент прикреплённый к роботу
        /// </summary>
        public IInstrument Instrument { get { return _instrument; } }

        /// <summary>
        /// Возвращает шасси, прикрепленное к роботу
        /// </summary>
        public IShassis Shassis { get { return _shassis; } }

        /// <summary>
        /// Флаг перемещения вперёд. Если установлено false, то робот перемещается обратно в сторону базы
        /// </summary>
        public bool ForwardFlag
        {
            get { return _forwardFlag; }
            set
            {
                SwitchForwardStateAction(this, new GenericEventArgs<bool>(value));
                _forwardFlag = value;
            }
        }

        /// <summary>
        /// Флаг определяющий включен ли инструмент
        /// </summary>
        public bool InstrumentFlag
        {
            get { return _usingInstrumentFlag; }
            set
            {
                SwitchInstrumentStateAction(this, new GenericEventArgs<bool>(value));
                _usingInstrumentFlag = value;
            }
        }

        #endregion

        #region Unity messages

        void Awake()
        {
            _eventArgs = new GenericEventArgs<Robot>(GetComponent<Robot>());
            _transform = GetComponent<Transform>();
            _shassis = GetComponent<IShassis>();
            _instrument = GetComponent<IInstrument>();
            _render = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            OnStoppageAction();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Прикрепить инструмент к роботу
        /// </summary>
        /// <param name="code">Код инструмента</param>
        public void SetInstrument(InstrumentsEnum code)
        {
            Type instrument = UtilityFunctions.TranslateInstrumentCodeToType(code);
            _instrument = gameObject.AddComponent(instrument) as IInstrument;
        }

        /// <summary>
        /// Прикрепить шасси к роботу
        /// </summary>
        /// <param name="code">Код шасси</param>
        public void SetShassis(ShassisEnum code)
        {
            Type shassis = UtilityFunctions.TranslateShassisCodeToType(code);
            _shassis = gameObject.AddComponent(shassis) as IShassis;
        }

        /// <summary>
        /// Вызов события остановки робота
        /// </summary>
        public void OnStoppageAction()
        {
            StopAllCoroutines();

            _movingFlag = false;
            ForwardFlag = false;
            InstrumentFlag = false;

            GameObject sprite = Instantiate(deadRobot, _transform.position, Quaternion.identity) as GameObject;
            sprite.GetComponent<SpriteRenderer>().sprite = _shassis.ShassisSprite;

            StoppageAction(this, _eventArgs);

            SetVisibility(false);
        }

        /// <summary>
        /// Вызов события возврата на базу
        /// </summary>
        public void OnReturnAction()
        {
            StopAllCoroutines();

            _movingFlag = false;
            ForwardFlag = false;
            InstrumentFlag = false;

            MoveTo(new Vector3(0, 0, 1));
            SetVisibility(false);
            GameManager.instance.CheckBatterys();

            ReturnAction(this, _eventArgs);
        }

        /// <summary>
        /// Переместить робота
        /// </summary>
        /// <remarks>Нужно, чтобы лишний раз не вызывать Transform</remarks>
        /// <param name="coor">Конечные координаты</param>
        public void MoveTo(Vector3 coor)
        {
            _transform.position = coor;
        }

        /// <summary>
        /// Сменить значение флага инструмента
        /// </summary>
        public void SwitchInstrument()
        {
            InstrumentFlag = !InstrumentFlag;
        }

        /// <summary>
        /// Робот начнёт передвижение
        /// </summary>
        /// <returns>Сопрограмма</returns>
        public IEnumerator StartMove()
        {
            InstrumentFlag = false;
            _movingFlag = true;
            ForwardFlag = true;

            InvokeAction(this, _eventArgs);

            yield return new WaitForSeconds(2);

            StartCoroutine(MakeStep());
        }

        /// <summary>
        /// Задаёт "видимость" робота.
        /// </summary>
        /// <param name="visible">false если робот должен быть невидим и не должен взаимодействовать с другими объектами.</param>
        public void SetVisibility(bool visible)
        {
            _render.enabled = visible;
            _collider.enabled = visible;
        }

        #endregion

        #region others

        IEnumerator MakeStep()
        {
            while (true)
            {
                bool flyFlag = _shassis.CheckForFly();

                if(_usingInstrumentFlag && !flyFlag)
                    yield return StartCoroutine(_instrument.Use(_forwardFlag));

                if(_movingFlag)
                    yield return StartCoroutine(_shassis.Move(_forwardFlag));

                yield return null;
            }
        }

        #endregion
    }
}

