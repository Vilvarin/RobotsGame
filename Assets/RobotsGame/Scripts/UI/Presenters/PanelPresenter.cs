using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор для модальных окон
    /// </summary>
    public class PanelPresenter : MonoBehaviour
    {
        public Panel constructor;
        public Panel control;

        public ReedyButton closeConstructorButton;

        GameManager _game;

        void Awake()
        {
            _game = GameManager.instance;

            _game.LevelLoaded += game_LevelLoaded;
            constructor.CloseAction += constructor_CloseAction;
            constructor.OpenAction += constructor_OpenAction;

            closeConstructorButton.ClosePanelAction += closeConstructorButton_ClosePanelAction;
        }

        void closeConstructorButton_ClosePanelAction(object sender, EventArgs e)
        {
            constructor.Close();
        }

        void game_LevelLoaded(object sender, EventArgs e)
        {
            constructor.Open();
        }

        void constructor_OpenAction(object sender, EventArgs e)
        {
            control.Close();
        }

        void constructor_CloseAction(object sender, EventArgs e)
        {
            control.Open();
        }

        void OnDestroy()
        {
            _game.LevelLoaded -= game_LevelLoaded;
            constructor.CloseAction -= constructor_CloseAction;
            constructor.OpenAction -= constructor_OpenAction;
        }
    }
}

