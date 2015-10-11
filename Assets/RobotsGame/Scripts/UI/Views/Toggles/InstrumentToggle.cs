using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Переключатель инструмента
    /// </summary>
    public class InstrumentToggle : MonoBehaviour
    {
        /// <summary>Код инструмента, закрепленного за переключателем</summary>
        public InstrumentsEnum instrument;

        /// <summary>Событие вызываемое при клике на переключатель. Передаёт код инструмента</summary>
        public event EventHandler<GenericEventArgs<InstrumentsEnum>> ClickAction = delegate { };

        GenericEventArgs<InstrumentsEnum> _instrumentArgs;

        void Awake()
        {
            _instrumentArgs = new GenericEventArgs<InstrumentsEnum>(instrument);
        }

        void Start()
        {
            GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                ClickAction(this, _instrumentArgs);
            });
        }
    }
}

