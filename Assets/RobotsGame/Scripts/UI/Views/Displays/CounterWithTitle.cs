using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Представление для счётчика с заголовком
    /// </summary>
    public class CounterWithTitle : MonoBehaviour
    {
        /// <summary>Заголовок счётчика</summary>
        public string title = "";

        Text _text;

        void Start()
        {
            _text = GetComponent<Text>();
            _text.text = title;
        }

        /// <summary>
        /// Обновление счётчика
        /// </summary>
        /// <typeparam name="T">Тип для нового значения</typeparam>
        /// <param name="count">Новое значение</param>
        public void UpdateCounter<T>(T count)
        {
            _text.text = title + count.ToString();
        }
    }
}

