using System;
using System.Collections;
using UnityEngine;

namespace RobotsGame
{
    /// <summary>
    /// Скрипт для блока ресурсов
    /// </summary>
    public class Collected : MonoBehaviour
    {
        public static int ResourceCount { get; set; }

        SoundPlayer _sound;

        /// <summary>
        /// Количество очков, которое приносит ресурс
        /// </summary>
        public int points = 10;

        public AudioClip[] collectClips;

        void Start()
        {
            _sound = GetComponent<SoundPlayer>();
        }

        /// <summary>
        /// Собрать ресурс
        /// </summary>
        public void Collect()
        {
            _sound.PlayRandomClip(collectClips);
            GameManager.instance.Batterys += points;
            ResourceCount--;

            if (ResourceCount <= 0)
                GameManager.instance.NextLevel();

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            Destroy(gameObject, 2f);
        }
    }
}
