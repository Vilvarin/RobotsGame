using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Проигрыватель звука для робота
    /// </summary>
    public class RobotSoundPlayer : SoundPlayer
    {
        /// <summary>
        /// Звук при гибели
        /// </summary>
        public AudioClip crashSound;
        /// <summary>
        /// Звук при вызове
        /// </summary>
        public AudioClip invokeSound;
        /// <summary>
        /// Звук при возврате на базу
        /// </summary>
        public AudioClip returnSound;

        Robot _robot;

        protected override void Start()
        {
            _source = GetComponent<AudioSource>();
            _robot = GetComponent<Robot>();

            _robot.ReturnAction += _robot_ReturnAction;
            _robot.StoppageAction += _robot_StoppageAction;
            _robot.InvokeAction += _robot_InvokeAction;
        }

        void _robot_InvokeAction(object sender, GenericEventArgs<Robot> e)
        {
            _source.PlayOneShot(invokeSound);
        }

        void _robot_StoppageAction(object sender, GenericEventArgs<Robot> e)
        {
            _source.PlayOneShot(crashSound);
        }

        void _robot_ReturnAction(object sender, GenericEventArgs<Robot> e)
        {
            _source.PlayOneShot(returnSound);
        }
    }
}

