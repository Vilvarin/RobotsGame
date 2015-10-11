using System;
using UnityEngine;
using UnityEngine.UI;

namespace RobotsGame.UI
{
    /// <summary>
    /// Панель с возможность выбрать робота
    /// </summary>
    public class ChangeRobotPanel : PanelWithRobots
    {
        /// <summary>
        /// Добавить кнопку вызова робота
        /// </summary>
        /// <param name="robot">Вызываемый робот</param>
        public override RectTransform AddRobot(Robot robot)
        {
            RectTransform instance = base.AddRobot(robot);

            instance.GetComponent<RobotButton>().FixRobot(robot);

            return instance;
        }
    }
}
