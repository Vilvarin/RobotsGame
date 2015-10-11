using UnityEngine;
using System.Collections;

namespace RobotsGame.UI
{
    /// <summary>
    /// Медиатор для отображения счётчиков
    /// </summary>
    public class CountersPresenter : MonoBehaviour 
    {
        public CounterWithTitle batterysCounter;
        public CounterWithTitle levelCounter;
        public CounterWithTitle robotsCounter;
        public RobotFabric fabric;

        GameManager _game;

        void Awake()
        {
            _game = GameManager.instance;

            _game.ChangeBatterys += _game_ChangeBatterys;
            _game.ChangeLevel += _game_ChangeLevel;
            fabric.AddRobotAction += fabric_AddRobotAction;
        }

        IEnumerator OnLevelWasLoaded()
        {
            yield return new WaitForEndOfFrame();
            robotsCounter.UpdateCounter<int>(fabric.maxRobotsCount);
        }

        void fabric_AddRobotAction(object sender, GenericEventArgs<Robot> e)
        {
            robotsCounter.UpdateCounter<int>(fabric.ModuloOfRobots);
        }

        void _game_ChangeLevel(object sender, GenericEventArgs<int> e)
        {
            levelCounter.UpdateCounter<int>(e.Value);
        }

        void _game_ChangeBatterys(object sender, GenericEventArgs<int> e)
        {
            batterysCounter.UpdateCounter<int>(e.Value);
        }

        void OnDestroy()
        {
            _game.ChangeBatterys -= _game_ChangeBatterys;
            _game.ChangeLevel -= _game_ChangeLevel;
        }
    }
}

