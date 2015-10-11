using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Вспомогательные статические функции
    /// </summary>
    public static class UtilityFunctions
    {
        #region leveling functions

        /// <summary>
        /// Выравнивание блока по координатной сетке
        /// </summary>
        /// <param name="block">Выравниваемый блок</param>
        public static void Leveling(Transform block)
        {
            float xCoor = Mathf.Round(block.position.x);
            float yCoor = Mathf.Round(block.position.y);
            block.position = new Vector3(xCoor, yCoor, block.position.z);
        }

        /// <summary>
        /// Выровнять точку по координатной сетке
        /// </summary>
        /// <param name="point">Выравниваемые координаты</param>
        /// <returns>Выровненные координаты</returns>
        public static Vector3 Leveling(Vector3 point)
        {
            float xCoor = Mathf.Round(point.x);
            float yCoor = Mathf.Round(point.y);
            return new Vector3(xCoor, yCoor, point.z);
        }

        #endregion

        #region Translating shassis and instruments

        /// <summary>
        /// Определяет тип инструмента по его коду
        /// </summary>
        /// <param name="instrument">Код инструмента</param>
        /// <returns>Тип инструмента</returns>
        public static Type TranslateInstrumentCodeToType(InstrumentsEnum instrument)
        {
            Type instrumentType = null;

            switch (instrument)
            {
                case InstrumentsEnum.None:
                    instrumentType = typeof(NullInstrument);
                    break;
                case InstrumentsEnum.Bomb:
                    instrumentType = typeof(BombInstrument);
                    break;
                case InstrumentsEnum.Collect:
                    instrumentType = typeof(CollectInstrument);
                    break;
                case InstrumentsEnum.Drill:
                    instrumentType = typeof(DrillInstrument);
                    break;
                case InstrumentsEnum.Dozer:
                    instrumentType = typeof(DozerInstrument);
                    break;
                default:
                    throw new InvalidOperationException("Получен незарегистрированный код инструмента");
            }

            return instrumentType;
        }

        /// <summary>
        /// Определяет тип шасси по его коду
        /// </summary>
        /// <param name="shassis">Код шасси</param>
        /// <returns>Тип шасси</returns>
        public static Type TranslateShassisCodeToType(ShassisEnum shassis)
        {
            Type shassisType = null;

            switch (shassis)
            {
                case ShassisEnum.None:
                    shassisType = typeof(NullShassis);
                    break;
                case ShassisEnum.Fly:
                    shassisType = typeof(FlyShassis);
                    break;
                case ShassisEnum.Jump:
                    shassisType = typeof(JumpShassis);
                    break;
                case ShassisEnum.Wheel:
                    shassisType = typeof(WheelShassis);
                    break;
                default:
                    throw new InvalidOperationException("Получен незарегистрированный код шасси");
            }

            return shassisType;
        }

        /// <summary>
        /// Оределяет название инструмента по его коду
        /// </summary>
        /// <param name="instrument">Код инструмента</param>
        /// <returns>Название инструмента</returns>
        public static string TranslateInstrumentCodeToName(InstrumentsEnum instrument)
        {
            string instrumentName = "";

            switch (instrument)
            {
                case InstrumentsEnum.None:
                    instrumentName = NullInstrument.instrumentName;
                    break;
                case InstrumentsEnum.Bomb:
                    instrumentName = BombInstrument.instrumentName;
                    break;
                case InstrumentsEnum.Collect:
                    instrumentName = CollectInstrument.instrumentName;
                    break;
                case InstrumentsEnum.Drill:
                    instrumentName = DrillInstrument.instrumentName;
                    break;
                case InstrumentsEnum.Dozer:
                    instrumentName = DozerInstrument.instrumentName;
                    break;
                default:
                    throw new InvalidOperationException("Получен незарегистрированный код инструмента");
            }

            return instrumentName;
        }

        /// <summary>
        /// Определяет название шасси по его коду
        /// </summary>
        /// <param name="shassis">Код шасси</param>
        /// <returns>Название шасси</returns>
        public static string TranslateShassisCodeToName(ShassisEnum shassis)
        {
            string shassisName = "";

            switch (shassis)
            {
                case ShassisEnum.None:
                    shassisName = NullShassis.shassisName;
                    break;
                case ShassisEnum.Fly:
                    shassisName = FlyShassis.shassisName;
                    break;
                case ShassisEnum.Jump:
                    shassisName = JumpShassis.shassisName;
                    break;
                case ShassisEnum.Wheel:
                    shassisName = WheelShassis.shassisName;
                    break;
                default:
                    throw new InvalidOperationException("Получен незарегистрированный код шасси");
            }

            return shassisName;
        }

        /// <summary>
        /// Определяет спрайт шасси по его коду
        /// </summary>
        /// <param name="shassis">Код шасси</param>
        /// <returns>Спрайт шасси</returns>
        public static Sprite TranslateShassisCodeToSprite(ShassisEnum shassis)
        {
            Sprite sprite = null;

            switch (shassis)
            {
                case ShassisEnum.None:
                    sprite = null;
                    break;
                case ShassisEnum.Fly:
                    sprite = SpriteStorage.instance.flyShassisSprite;
                    break;

                case ShassisEnum.Jump:
                    sprite = SpriteStorage.instance.jumpShassisSprite;
                    break;

                case ShassisEnum.Wheel:
                    sprite = SpriteStorage.instance.wheelShassisSprite;
                    break;

                default:
                    throw new InvalidOperationException("Получен незарегистрированный код инструмента");
            }

            return sprite;
        }

        #endregion
    }
}

