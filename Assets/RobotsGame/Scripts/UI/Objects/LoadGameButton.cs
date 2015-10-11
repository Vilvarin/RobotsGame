using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Скрипт для кнопки загрузки
    /// </summary>
    /// <remarks>Деактивирует кнопку если нет данных для загрузки</remarks>
    public class LoadGameButton : MonoBehaviour 
    {
        void Start()
        {
            if (!PlayerPrefs.HasKey("Batterys") || !PlayerPrefs.HasKey("Level"))
                GetComponent<Button>().interactable = false;
        }
    }
}

