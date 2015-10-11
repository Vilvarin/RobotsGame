using UnityEngine;
using System;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Переходы между уровнями, начало и конец игры, сохранения
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region fields and properties

        public static GameManager instance;

        /// <summary>Стартовые батарейки игрока</summary>
        public int startBatterys = 20;
        private int _batterys;
        /// <summary>Текущее количество батареек игрока</summary>
        public int Batterys
        {
            get
            {
                return _batterys;
            }

            set
            {
                ChangeBatterys(this, new GenericEventArgs<int>(value));
                _batterys = value;
            }
        }

        /// <summary>Стартовый уровень</summary>
        public int startLevel = 1;
        private int _level;
        /// <summary>Текущий уровень</summary>
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                ChangeLevel(this, new GenericEventArgs<int>(value));
                _level = value;
            }
        }

        #endregion

        #region events

        /// <summary>Событие вызываемое при изменении уровня</summary>
        public event EventHandler<GenericEventArgs<int>> ChangeLevel = delegate { };
        /// <summary>Событие вызываемое при изменении количества батареек</summary>
        public event EventHandler<GenericEventArgs<int>> ChangeBatterys = delegate { };
        /// <summary>Событие вызывается при загрузке нового уровня</summary>
        public event EventHandler LevelLoaded = delegate { };

        #endregion

        #region Unity messages

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        IEnumerator OnLevelWasLoaded(int scene)
        {
            yield return new WaitForEndOfFrame();
            ChangeLevel(this, new GenericEventArgs<int>(Level));
            ChangeBatterys(this, new GenericEventArgs<int>(Batterys));
            LevelLoaded(this, EventArgs.Empty);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ExitGame();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Окончание игры
        /// </summary>
        public void GameOver()
        {
            PlayerPrefs.DeleteAll();
            Application.LoadLevel("GameOverMenu");
        }

        /// <summary>
        /// Переход между уровнями
        /// </summary>
        public void NextLevel()
        {
            Level++;
            SaveGame();
            Application.LoadLevel("Scene");
        }

        /// <summary>
        /// Новая игра
        /// </summary>
        public void NewGame()
        {
            Level = 1;
            Batterys = startBatterys;
            SaveGame();
            Application.LoadLevel("Scene");
        }

        /// <summary>
        /// Перезапуск уровня
        /// </summary>
        public void RestartLevel()
        {
            LoadGame();
            Application.LoadLevel("Scene");
        }

        /// <summary>
        /// Выход из игры
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Сохранение игры в PlayerPrefs
        /// </summary>
        public void SaveGame()
        {
            PlayerPrefs.SetInt("Batterys", Batterys);
            PlayerPrefs.SetInt("Level", Level);
        }

        /// <summary>
        /// Загрузка игры из PlayerPrefs
        /// </summary>
        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("Batterys"))
                Batterys = PlayerPrefs.GetInt("Batterys");
            if (PlayerPrefs.HasKey("Level"))
                Level = PlayerPrefs.GetInt("Level");
        }
        
        /// <summary>
        /// Проверить количество батареек
        /// </summary>
        public void CheckBatterys()
        {
            if (Batterys <= 0)
            {
                GameOver();
            }
        }

        #endregion
    }
}

