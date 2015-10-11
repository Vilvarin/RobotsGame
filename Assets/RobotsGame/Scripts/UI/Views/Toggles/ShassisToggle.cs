using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Переключатель шасси
    /// </summary>
    public class ShassisToggle : MonoBehaviour
    {
        /// <summary>Код шасси, за которым закреплён переключатель</summary>
        public ShassisEnum shassis;
        
        /// <summary>Событие, вызываемое при клике на переключателе. Передаёт код шасси</summary>
        public event EventHandler<GenericEventArgs<ShassisEnum>> ClickAction = delegate { };

        GenericEventArgs<ShassisEnum> _shassisArgs;

        void Awake()
        {
            _shassisArgs = new GenericEventArgs<ShassisEnum>(shassis);
        }

        void Start()
        {
            GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                ClickAction(this, _shassisArgs);
            });
        }
    }
}

