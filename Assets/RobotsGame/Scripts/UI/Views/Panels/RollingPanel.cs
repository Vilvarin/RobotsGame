using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Закрывающаяся панель с возможностью её свернуть.
    /// </summary>
    public class RollingPanel : Panel
    {
        bool _maxFlag = true;

        /// <summary>
        /// Свернуть панель
        /// </summary>
        public virtual void Minimize()
        {
            gameObject.SetActive(false);
            _maxFlag = false;
        }

        /// <summary>
        /// Развернуть панель
        /// </summary>
        public virtual void Maximize()
        {
            gameObject.SetActive(true);
            _maxFlag = true;
        }

        public void Roll()
        {
            if (_maxFlag)
                Minimize();
            else
                Maximize();
        }
    }
}

