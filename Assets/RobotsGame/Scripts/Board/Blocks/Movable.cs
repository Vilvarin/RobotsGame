using System;
using System.Collections;
using UnityEngine;

namespace RobotsGame
{
    /// <summary>
    /// Скрипт перемещаемого блока
    /// </summary>
    public class Movable : MonoBehaviour
    {
        /// <summary>
        /// Скорость перемещения
        /// </summary>
        public float MovingSpeed = 0.2f;

        public AudioClip moveClip;

        Transform _transform;
        AudioSource _source;

        void Start()
        {
            _transform = GetComponent<Transform>();
            _source = GetComponent<AudioSource>();
            StartCoroutine(Falling());
        }

        /// <summary>
        /// Передвинуть
        /// </summary>
        /// <param name="direction">Направление движения</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator MoveTo(Vector3 direction)
        {
            if (BoardModel.CheckForEmpty(_transform.position + direction))
            {
                _source.PlayOneShot(moveClip);

                float t = 0;
                Vector3 pos = _transform.position;

                while (t <= MovingSpeed)
                {
                    _transform.position = Vector3.Lerp(pos, pos + direction, t / MovingSpeed);
                    t += Time.deltaTime;
                    yield return null;
                }

                UtilityFunctions.Leveling(_transform);
            }
        }

        IEnumerator Falling()
        {
            while (true)
            {
                Collider2D hit = Physics2D.OverlapPoint(_transform.position + new Vector3(0, -0.51f, 0));

                if (hit == null)
                    yield return StartCoroutine(MoveTo(Vector3.down));
                else if (hit.tag == "Robot")
                    hit.GetComponent<Robot>().OnStoppageAction();

                yield return null;
            }
        }
    }
}
