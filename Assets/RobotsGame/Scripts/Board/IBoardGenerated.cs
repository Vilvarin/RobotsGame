using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsGame
{
    /// <summary>
    /// Интерфейс для стратегии генерации поля
    /// </summary>
    public interface IBoardGenerating
    {
        /// <summary>
        /// Набор всех тайлов
        /// </summary>
        Tile[] AllTiles { get; }

        /// <summary>
        /// Тайлы с платформами
        /// </summary>
        List<Tile> PlatformTiles { get; }

        /// <summary>
        /// Количество сгенерированных ресурсов
        /// </summary>
        int ResourceCount { get; }

        /// <summary>
        /// Создать поле
        /// </summary>
        void CreateBoard();
    }
}
