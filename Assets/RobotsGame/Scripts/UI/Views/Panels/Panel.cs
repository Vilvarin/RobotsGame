using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Класс для модальных окон и панелей с возможностью закрытия.
    /// </summary>
    public class Panel : MonoBehaviour
    {
        public event EventHandler OpenAction = delegate { };
        public event EventHandler CloseAction = delegate { };

        public virtual void Open()
        {
            OpenAction(this, EventArgs.Empty);
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            CloseAction(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }
}

