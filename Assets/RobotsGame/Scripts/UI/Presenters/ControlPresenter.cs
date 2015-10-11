using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор панели контроля роботов
    /// </summary>
    public class ControlPresenter : MonoBehaviour
    {
        #region fields

        public RobotFabric fabric;
        public RobotCommander commandHandler;
        public BoardModel board;

        public ChangeRobotPanel robotPanel;
        public Button restartButton;
        public InstrumentButton instrumentButton;
        public Button returnButton;

        Platform[] _platforms;
        List<RobotButton> _robotButtons = new List<RobotButton>();
        List<Robot> _robots = new List<Robot>();

        #endregion

        #region init

        void Awake()
        {
            fabric.AddRobotAction += fabric_AddRobotAction;
            commandHandler.StartMoveAction += commandHandler_StartMoveAction;
            commandHandler.SwitchForwardStateAction += commandHandler_SwitchForwardStateAction;
            commandHandler.SwitchInstrumentStateAction += commandHandler_SwitchInstrumentStateAction;

            restartButton.onClick.AddListener(restartButton_ClickAction);
            returnButton.onClick.AddListener(returnButton_ClickAction);
            instrumentButton.ClickAction += instrumentButton_ClickAction;
        }        
        
        void Start()
        {
            _platforms = board.GetPlatforms();

            foreach (Platform platform in _platforms)
            {
                platform.MouseEnterAction += platform_MouseEnterAction;
                platform.MouseExitAction += platform_MouseExitAction;
                platform.ClickAction += platform_ClickAction;
            }
        }

        #endregion

        #region buttons

        void returnButton_ClickAction()
        {
            commandHandler.ActiveRobot.ForwardFlag = false;
        }

        void instrumentButton_ClickAction(object sender, EventArgs e)
        {
            if (commandHandler.IsActive)
                commandHandler.ActiveRobot.SwitchInstrument();       
        }        
        
        void robotButton_ClickAction(object sender, GenericEventArgs<Robot> e)
        {
            foreach (Platform platform in _platforms)
                platform.LightenPlatform();

            commandHandler.ActiveRobot = e.Value;
        }
        
        void restartButton_ClickAction()
        {
            GameManager.instance.RestartLevel();
        }

        #endregion

        #region active robot

        void commandHandler_SwitchInstrumentStateAction(object sender, GenericEventArgs<bool> e)
        {
            instrumentButton.ChangeState(e.Value);
        }

        void commandHandler_SwitchForwardStateAction(object sender, GenericEventArgs<bool> e)
        {
            returnButton.interactable = e.Value;
        }

        void commandHandler_StartMoveAction(object sender, GenericEventArgs<Robot> e)
        {
            foreach (Platform platform in _platforms)
                platform.DarkenPlatform();

            foreach (RobotButton button in _robotButtons)
            {
                if (button.FixedRobot == e.Value)
                    button.Deactivate();
            }
        }
        
        #endregion

        #region platform

        void platform_ClickAction(object sender, GenericEventArgs<Platform> e)
        {
            if (commandHandler.IsActive)
                commandHandler.StartRobotMove(e.Value);
        }

        void platform_MouseExitAction(object sender, EventArgs e)
        {
            if (commandHandler.IsActive)
                commandHandler.RemoveRobotFromPlatform();
        }

        void platform_MouseEnterAction(object sender, GenericEventArgs<Platform> e)
        {
            if (commandHandler.IsActive)
                commandHandler.EstablishRobotOnPlatform(e.Value);
        }

        #endregion

        void fabric_AddRobotAction(object sender, GenericEventArgs<Robot> e)
        {
            RobotButton robotButton = robotPanel.AddRobot(e.Value).GetComponent<RobotButton>();
            _robotButtons.Add(robotButton);
            robotButton.ClickAction += robotButton_ClickAction;
            e.Value.ReturnAction += robots_ReturnAction;
        }

        void robots_ReturnAction(object sender, GenericEventArgs<Robot> e)
        {
            foreach (RobotButton button in _robotButtons)
            {
                if (button.FixedRobot == e.Value)
                    button.Activate();
            }
        }

        void OnDestroy()
        {
            fabric.AddRobotAction -= fabric_AddRobotAction;
            commandHandler.StartMoveAction -= commandHandler_StartMoveAction;
            commandHandler.SwitchForwardStateAction -= commandHandler_SwitchForwardStateAction;
            commandHandler.SwitchInstrumentStateAction -= commandHandler_SwitchInstrumentStateAction;

            restartButton.onClick.RemoveListener(restartButton_ClickAction);

            foreach (RobotButton button in _robotButtons)
                button.ClickAction -= robotButton_ClickAction;

            foreach (Robot robot in _robots)
            {
                robot.ReturnAction -= robots_ReturnAction;
            }

            foreach (Platform platform in _platforms)
            {
                platform.ClickAction -= platform_ClickAction;
                platform.MouseEnterAction -= platform_MouseEnterAction;
                platform.MouseExitAction -= platform_MouseExitAction;
            }
        }
    }
}

