using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инструмент бурильщика. Позволяет разрушать блоки.
    /// </summary>
    public class DrillInstrument : MonoBehaviour, IInstrument
    {
        Transform _transform;
        SoundPlayer _sound;
        
        #region properties

        public InstrumentsEnum InstrumentCode
        {
            get { return InstrumentsEnum.Drill; }
        }

        public static readonly string instrumentName = "бурильщик";
        public string InstrumentName
        {
            get { return instrumentName; }
        }

        #endregion

        void Start()
        {
            _transform = GetComponent<Transform>();
            _sound = GetComponent<SoundPlayer>();
        }

        /// <summary>
        /// Использовать инструмент
        /// </summary>
        /// <param name="direction">Направление движения шасси</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator Use(bool direction)
        {
            if (direction)
                yield return StartCoroutine(DigToRight());
            else
                yield return StartCoroutine(DigToLeft());
        }

        IEnumerator DigToRight()
        {
            Destructible block;

            if (BoardModel.GetBlock<Destructible>(_transform.position + Vector3.right, out block))
            {
                block.Destruct(1);
                _sound.PlayRandomClip(ClipStorage.instance.drillInstrumentClips);
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator DigToLeft()
        {
            Destructible block;

            if (BoardModel.GetBlock<Destructible>(_transform.position + Vector3.left, out block))
            {
                block.Destruct(1);
                _sound.PlayRandomClip(ClipStorage.instance.drillInstrumentClips);
                yield return new WaitForSeconds(1);
            }
        }
    }
}

