using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Шасси по умолчанию
    /// </summary>
    public class NullShassis : MonoBehaviour, IShassis
    {
        public ShassisEnum ShassisCode
        {
            get { return ShassisEnum.None; }
        }

        public static readonly string shassisName = "Робот ";
        public string ShassisName
        {
            get { return shassisName; }
        }

        public Sprite ShassisSprite
        {
            get { return null; }
        }

        public bool CheckForFly()
        {
            return false;
        }

        public IEnumerator Move(bool direction)
        {
            yield return null;
        }

        public void RenderLine(LineRenderer lineRenderer, Vector3 startPoint)
        {

        }
    }
}

