using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор между стартовым меню и GameManager
    /// </summary>
    public class StartMenuPresenter : MonoBehaviour
    {
        public Button startButton;
        public Button loadButton;
        public Button howToButton;
        public Button exitButton;

        public Panel howToPanel;
        public Button closeHowToPanelButton;

        GameManager _game;

        void Start()
        {
            _game = GameManager.instance;

            startButton.onClick.AddListener(startButton_ClickAction);
            loadButton.onClick.AddListener(loadButton_ClickAction);
            exitButton.onClick.AddListener(exitButton_ClickAction);
            howToButton.onClick.AddListener(howToButton_ClickAction);
            closeHowToPanelButton.onClick.AddListener(closeHowToPanel_ClickAction);
        }

        void howToButton_ClickAction()
        {
            howToPanel.Open();
        }

        void closeHowToPanel_ClickAction()
        {
            howToPanel.Close();
        }

        void exitButton_ClickAction()
        {
            _game.ExitGame();
        }

        void loadButton_ClickAction()
        {
            _game.LoadGame();
            _game.RestartLevel();
        }

        void startButton_ClickAction()
        {
            _game.NewGame();
        }

        void OnDestroy()
        {
            startButton.onClick.RemoveListener(startButton_ClickAction);
            loadButton.onClick.RemoveListener(loadButton_ClickAction);
            exitButton.onClick.RemoveListener(exitButton_ClickAction);
        }
    }
}

