using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Хранит блоки игрового поля и статические функции для обращения к ним.
    /// </summary>
    public class BoardModel : MonoBehaviour
    {
        //Стратегия для генерации поля. Прикрепляется как компонент к игровому объекту поля вместе с BoardModel
        IBoardGenerating _boardGenerator;

        Tile[] _allTiles;
        Tile[] _platformTiles;

        void Awake()
        {
            _boardGenerator = GetComponent<IBoardGenerating>();
        }

        void Start()
        {
            _boardGenerator.CreateBoard();
            _allTiles = _boardGenerator.AllTiles;
            _platformTiles = _boardGenerator.PlatformTiles.ToArray();
            Collected.ResourceCount = _boardGenerator.ResourceCount;

            foreach (Tile tile in _platformTiles)
            {
                if (!tile.block.GetComponent<Platform>())
                    throw new MissingComponentException("В блоке платформы отсутствует соответствующий компонент. Проверьте префаб, используемый в качестве прототипа.");
            }
        }

        /// <summary>
        /// Получить блоки с платформами
        /// </summary>
        /// <returns>Массив ссылок на платформы</returns>
        public Platform[] GetPlatforms()
        {
            Platform[] platforms = new Platform[_platformTiles.Length];

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i] = _platformTiles[i].block.GetComponent<Platform>();
            }

            return platforms;
        }

        #region static function

        /// <summary>
        /// Проверить пуст ли блок
        /// </summary>
        /// <param name="point">Точка для рэйкастинга</param>
        /// <returns>true если пуст</returns>
        public static bool CheckForEmpty(Vector3 point)
        {
            Collider2D hit = Physics2D.OverlapPoint((Vector2)point);

            return hit == null;
        }

        /// <summary>
        /// Проверить находится ли в тайле платформа
        /// </summary>
        /// <param name="point">Точка для рэйкастинга</param>
        /// <returns>true если есть платформа</returns>
        public static bool CheckForPlatform(Vector3 point)
        {
            Collider2D hit = Physics2D.OverlapPoint((Vector2)point, 1);

            if (hit == null)
                return false;

            return hit.tag == "Platform";
        }

        /// <summary>
        /// Проверить пуст ли тайл и есть ли на нём платформа
        /// </summary>
        /// <param name="point">Точка для рэйкастинга</param>
        /// <returns>true если блок пуст или на нём стоит платформа</returns>
        public static bool CheckForEmptyOrPlatform(Vector3 point)
        {
            Collider2D hit = Physics2D.OverlapPoint((Vector2)point);

            if (hit == null)
                return true;

            return hit.tag == "Platform";
        }

        /// <summary>
        /// Получить ссылку на блок
        /// </summary>
        /// <typeparam name="T">Тип искомого блока</typeparam>
        /// <param name="point">Точка для рэйкастинга</param>
        /// <param name="block">Переменная в которую вернётся найденный блок</param>
        /// <returns>true если найден блок искомого типа</returns>
        public static bool GetBlock<T>(Vector3 point, out T block) where T : Component
        {
            block = default(T);
            Collider2D hit = Physics2D.OverlapPoint((Vector2)point);

            if (hit != null && hit.GetComponent<T>())
            {
                block = hit.GetComponent<T>();
                return true;
            }

            return false;
        }

        #endregion
    }
}

