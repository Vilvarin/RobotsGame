using System;
using System.Collections;
using UnityEngine;

namespace RobotsGame
{
    /// <summary>
    /// Скрипт для бомбы
    /// </summary>
    public class Bomb : MonoBehaviour
    {
        /// <summary>
        /// Время перед взрывом
        /// </summary>
        public float timer = 5;

        public AudioClip boomClip;

        Transform _transform;
        AudioSource _source;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            _source = GetComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(timer);

            Boom();
            Destroy(gameObject, 1f);
        }

        void Boom()
        {
            _source.PlayOneShot(boomClip);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Destructible block;

                    if (BoardModel.GetBlock<Destructible>(_transform.position + new Vector3(x, y, 0), out block))
                    {
                        block.Destruct(2);
                    }
                        
                }
            }
        }
    }
}
