using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Интерфейс инструментов
    /// </summary>
    public interface IInstrument
    {
        /// <summary>Код инструмента в перечислении</summary>
        InstrumentsEnum InstrumentCode { get; }
        /// <summary>Название инструмента</summary>
        string InstrumentName { get; }

        /// <summary>
        /// Использовать инструмент
        /// </summary>
        /// <param name="direction">Направление движения шасси</param>
        /// <returns>Сопрограмма</returns>
        IEnumerator Use(bool direction);
    }
}

