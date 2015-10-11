using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Хранилище игровых объектов
    /// </summary>
    public class ObjectStorage : MonoBehaviour
    {
        public static ObjectStorage instance { get; private set; }

        public Bomb bomb;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}

