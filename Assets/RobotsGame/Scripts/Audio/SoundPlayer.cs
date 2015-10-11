using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Базовый проигрыватель звука
    /// </summary>
    public class SoundPlayer : MonoBehaviour
    {
        protected AudioSource _source;

        protected virtual void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Сыграть случайный клип из массива клипов
        /// </summary>
        /// <param name="clips"></param>
        public virtual void PlayRandomClip(AudioClip[] clips)
        {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            _source.PlayOneShot(clip);
        }
    }
}

