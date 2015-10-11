using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Класс для клетки поля
    /// </summary>
    public class Tile
    {
        /// <summary>Координаты клетки</summary>
        public Vector2 coor;
        /// <summary>Объект, находящийся в клетке</summary>
        public GameObject block;

        public Tile(Vector2 coor)
        {
            this.coor = coor;
        }
    }
}

