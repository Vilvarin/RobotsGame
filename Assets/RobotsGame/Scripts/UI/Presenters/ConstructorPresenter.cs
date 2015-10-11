using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор для представления панели конструктора
    /// </summary>
    public class ConstructorPresenter : MonoBehaviour
    {
        #region fields

        public RobotFabric fabric;

        public List<ShassisToggle> shassisToggles = new List<ShassisToggle>();
        public List<InstrumentToggle> instrumentToggles = new List<InstrumentToggle>();
        public ReedyButton reedyButton;
        public Button rollButton;

        public RollingPanel rollingPanel;
        public PanelWithRobots robotPanel;
        public PreviewImage previewImage;
        public PreviewText previewText;
        public Text errorText;

        #endregion

        void Awake()
        {
            foreach (ShassisToggle toggle in shassisToggles)
                toggle.ClickAction += shassisToggle_ClickAction;

            foreach (InstrumentToggle toggle in instrumentToggles)
                toggle.ClickAction += instrumentToggle_ClickAction;

            fabric.AddRobotAction += fabric_AddRobotAction;
            fabric.FullHandAction += fabric_FullHandAction;
            fabric.ErrorOfAddingAction += fabric_ErrorOfAddingAction;

            reedyButton.AddRobotAction += reedyButton_AddRobotAction;
            rollButton.onClick.AddListener(rollButton_ClickAction);
        }

        void reedyButton_AddRobotAction(object sender, EventArgs e)
        {
            fabric.AddRobot();
        }

        void rollButton_ClickAction()
        {
            rollingPanel.Roll();
        }

        #region robot fabric delegates

        void fabric_FullHandAction(object sender, EventArgs e)
        {
            reedyButton.SwitchState();
        }

        void fabric_ErrorOfAddingAction(object sender, EventArgs e)
        {
            errorText.text = "Чего-то не хватает";
        }

        void fabric_AddRobotAction(object sender, GenericEventArgs<Robot> e)
        {
            robotPanel.AddRobot(e.Value);
        }

        #endregion

        #region toggles

        void instrumentToggle_ClickAction(object sender, GenericEventArgs<InstrumentsEnum> e)
        {
            errorText.text = "";
            fabric.Instrument = e.Value;
            previewText.DisplayInstrument(UtilityFunctions.TranslateInstrumentCodeToName(e.Value));
        }

        void shassisToggle_ClickAction(object sender, GenericEventArgs<ShassisEnum> e)
        {
            errorText.text = "";
            fabric.Shassis = e.Value;
            previewImage.DisplaySprite(UtilityFunctions.TranslateShassisCodeToSprite(e.Value));
            previewText.DisplayShassis(UtilityFunctions.TranslateShassisCodeToName(e.Value));
        }

        #endregion

        void OnDestroy()
        {
            foreach (ShassisToggle toggle in shassisToggles)
                toggle.ClickAction -= shassisToggle_ClickAction;

            foreach (InstrumentToggle toggle in instrumentToggles)
                toggle.ClickAction -= instrumentToggle_ClickAction;

            fabric.AddRobotAction -= fabric_AddRobotAction;
            fabric.FullHandAction -= fabric_FullHandAction;
            fabric.ErrorOfAddingAction -= fabric_ErrorOfAddingAction;

            reedyButton.AddRobotAction -= reedyButton_AddRobotAction;
        }
    }
}
