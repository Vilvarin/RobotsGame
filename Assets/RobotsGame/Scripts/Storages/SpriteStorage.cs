using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Хранилище спрайтов. Одиночка.
    /// </summary>
    public class SpriteStorage : MonoBehaviour
    {
        public Sprite wheelShassisSprite;
        public Sprite flyShassisSprite;
        public Sprite jumpShassisSprite;

        public static SpriteStorage instance { get; private set; }

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}

