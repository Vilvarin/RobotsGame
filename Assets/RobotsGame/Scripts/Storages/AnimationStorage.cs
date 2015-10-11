using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Хрантлтще аниматоров
    /// </summary>
    public class AnimationStorage : MonoBehaviour
    {
        public static AnimationStorage instance { get; private set; }

        public RuntimeAnimatorController whellShassisAnimator;
        public RuntimeAnimatorController jumpShassisAnimator;
        public RuntimeAnimatorController flyShassisAnimator;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}

