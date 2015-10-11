using System;
using System.Collections;
using UnityEngine;

namespace RobotsGame
{
    /// <summary>
    /// Скрипт уничтожаемого блока
    /// </summary>
    public class Destructible : MonoBehaviour
    {
        /// <summary>
        /// Таймаут
        /// </summary>
        public float DestructTime = 1f;
        /// <summary>
        /// Прочность блока
        /// </summary>
        public int durability;

        public AudioClip crashClip;

        AudioSource _source;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Уничтожить блок
        /// </summary>
        /// <param name="power">Сила воздействия</param>
        public void Destruct(int power)
        {
            if (GetComponent<Collected>())
                Collected.ResourceCount--;

            if (power >= durability)
            {
                _source.PlayOneShot(crashClip);
                Destroy(gameObject, DestructTime);
            }
        }
    }
}
