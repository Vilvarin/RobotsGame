using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    public interface IShassis
    {
        /// <summary>Код шасси в перечислении</summary>
        ShassisEnum ShassisCode { get; }
        /// <summary>Название шасси</summary>
        string ShassisName { get; }
        /// <summary>Спрайт для шасси </summary>
        Sprite ShassisSprite { get; }

        /// <summary>
        /// Сдвинуться в следующий тайл
        /// </summary>
        /// <param name="direction">Направление движения. True если вперед.</param>
        /// <returns>Сопрограмма</returns>
        IEnumerator Move(bool direction);
        /// <summary>
        /// Проверяет находится ли робот в полёте или падении
        /// </summary>
        /// <returns>true если падаем</returns>
        bool CheckForFly();

        void RenderLine(LineRenderer lineRenderer, Vector3 startPoint);
    }
}

