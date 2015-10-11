using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор для интерфейса окна инструкции по игре
    /// </summary>
    public class HowToPanelPresenter : MonoBehaviour
    {
        /// <summary>
        /// Основная панель
        /// </summary>
        public Panel howToPanel;
        /// <summary>
        /// Кнопка перехода на следующий слайд
        /// </summary>
        public Button onwardButton;

        void Awake()
        {
            howToPanel.CloseAction += delegate
            {
                gameObject.SetActive(true);
            };

            if(onwardButton != null)
                onwardButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        }
    }
}

