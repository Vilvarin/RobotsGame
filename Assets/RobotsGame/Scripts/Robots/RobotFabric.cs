using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инстанцирует роботов из префаба, отслеживает общее их количество.
    /// </summary>
    public class RobotFabric : MonoBehaviour
    {
        #region fields

        /// <summary>Максимальное число роботов на уровне</summary>
        public int maxRobotsCount = 6;
        /// <summary>Текущее число роботов</summary>
        int _countOfRobots = 0;
        /// <summary>Сколько ещё роботов можно набрать</summary>
        public int ModuloOfRobots { get { return maxRobotsCount - _countOfRobots; } }

        /// <summary>Хранилище для шасси</summary>
        public ShassisEnum Shassis { get; set; }
        /// <summary>Хранилище для инструмента</summary>
        public InstrumentsEnum Instrument { get; set; }

        /// <summary>Прототип робота</summary>
        public Robot prototype;

        #endregion

        #region events

        /// <summary>Вызывается если достигнуто максимальное число роботов</summary>
        public event EventHandler FullHandAction = delegate { };
        /// <summary>Вызывается при ошибке создания робота</summary>
        public event EventHandler ErrorOfAddingAction = delegate { };
        /// <summary>Вызывается при создании нового робота</summary>
        public event EventHandler<GenericEventArgs<Robot>> AddRobotAction = delegate { };

        #endregion

        /// <summary>
        /// Инстанцирующий метод
        /// </summary>
        /// <param name="shassis">Код шасси</param>
        /// <param name="instrument">Код инструмента</param>
        /// <returns>Ссылка на созданного робота</returns>
        Robot CreateRobot(ShassisEnum shassis, InstrumentsEnum instrument)
        {
            Robot product = Instantiate(prototype, new Vector3(0, 0, 0), Quaternion.identity) as Robot;
            product.SetInstrument(instrument);
            product.SetShassis(shassis);
            product.SetVisibility(false);

            return product;
        }

        /// <summary>
        /// Создание робота из компонентов в хранилище
        /// </summary>
        public void AddRobot()
        {
            if (Shassis == ShassisEnum.None || Instrument == InstrumentsEnum.None)
                ErrorOfAddingAction(this, EventArgs.Empty);
            else
            {
                Robot robot = CreateRobot(Shassis, Instrument);
                _countOfRobots++;
                AddRobotAction(this, new GenericEventArgs<Robot>(robot));

                if (_countOfRobots >= maxRobotsCount)
                    FullHandAction(this, EventArgs.Empty);
            }
        }
    }
}

