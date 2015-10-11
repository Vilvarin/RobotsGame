using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инструмент подрывника. Позволяет установить бомбу 
    /// </summary>
    public class BombInstrument : MonoBehaviour, IInstrument
    {
        Transform _transform;
        Robot _robot;
        SoundPlayer _sound;

        #region properties

        InstrumentsEnum IInstrument.InstrumentCode
        {
            get { return InstrumentsEnum.Bomb; }
        }

        public static readonly string instrumentName = "подрывник";
        string IInstrument.InstrumentName
        {
            get { return instrumentName; }
        }

        #endregion

        void Start()
        {
            _transform = GetComponent<Transform>();
            _robot = GetComponent<Robot>();
            _sound = GetComponent<SoundPlayer>();
        }

        /// <summary>
        /// Использовать инструмент
        /// </summary>
        /// <param name="direction">Направление движения шасси</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator Use(bool direction)
        {
            yield return new WaitForSeconds(1f);

            Instantiate(ObjectStorage.instance.bomb, _transform.position, Quaternion.identity);
            _sound.PlayRandomClip(ClipStorage.instance.bomberInstrumentClips);
            _robot.SwitchInstrument();
        }
    }
}

