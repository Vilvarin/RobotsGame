using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Хранилище аудио
    /// </summary>
    public class ClipStorage : MonoBehaviour
    {
        public static ClipStorage instance { get; private set; }

        public AudioClip[] whellShassisClips;
        public AudioClip[] jumpShassisClips;
        public AudioClip[] flyShassisClips;

        public AudioClip[] drillInstrumentClips;
        public AudioClip[] bomberInstrumentClips;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}

