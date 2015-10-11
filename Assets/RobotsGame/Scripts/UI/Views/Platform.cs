using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RobotsGame
{
    /// <summary>
    /// Скрипт для стартовой платформы робота
    /// </summary>
    public class Platform : MonoBehaviour
    {
        #region fields

        /// <summary>Подсвеченный спрайт</summary>
        public Sprite lightSprite;
        /// <summary>Затененный спрайт</summary>
        public Sprite darkSprite;

        public event EventHandler<GenericEventArgs<Platform>> MouseEnterAction = delegate { };
        public event EventHandler MouseExitAction = delegate { };
        public event EventHandler<GenericEventArgs<Platform>> ClickAction = delegate { };

        SpriteRenderer _spriteRenderer;
        GenericEventArgs<Platform> _eventArgs;

        bool _lightenState = false;

        #endregion

        #region Unity messages

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _eventArgs = new GenericEventArgs<Platform>(GetComponent<Platform>());

            _spriteRenderer.sprite = darkSprite;
        }
        
        void OnMouseEnter()
        {
            if(_lightenState)
                MouseEnterAction(this, _eventArgs);
        }

        void OnMouseExit()
        {
            if(_lightenState)
                MouseExitAction(this, EventArgs.Empty);
        }

        void OnMouseDown()
        {
            if(_lightenState)
                ClickAction(this, _eventArgs);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Подсветить платформу
        /// </summary>
        public void LightenPlatform()
        {
            _spriteRenderer.sprite = lightSprite;
            _lightenState = true;
        }

        /// <summary>
        /// Затемнить платформу
        /// </summary>
        public void DarkenPlatform()
        {
            _spriteRenderer.sprite = darkSprite;
            _lightenState = false;
        }

        #endregion
    }
}
