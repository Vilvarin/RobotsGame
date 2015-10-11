using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инструмент бульдозера. Позволяет толкнуть блок на одну клетку.
    /// </summary>
    public class DozerInstrument : MonoBehaviour, IInstrument
    {
        Transform _transform;

        #region properties

        public InstrumentsEnum InstrumentCode
        {
            get { return InstrumentsEnum.Dozer; }
        }

        public static readonly string instrumentName = "бульдозер";
        public string InstrumentName
        {
            get { return instrumentName; }
        }

        #endregion

        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        /// <summary>
        /// Использовать инструмент
        /// </summary>
        /// <param name="direction">Направление движения шасси</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator Use(bool direction)
        {
            if (direction)
                yield return StartCoroutine(ShiftToRight());
            else
                yield return StartCoroutine(ShiftToLeft());
        }

        IEnumerator ShiftToRight()
        {
            Movable block;

            if (BoardModel.GetBlock<Movable>(_transform.position + Vector3.right, out block))
            {
                yield return StartCoroutine(block.MoveTo(Vector3.right));
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator ShiftToLeft()
        {
            Movable block;

            if (BoardModel.GetBlock<Movable>(_transform.position + Vector3.left, out block))
            {
                yield return StartCoroutine(block.MoveTo(Vector3.left));
                yield return new WaitForSeconds(1);
            }
        }
    }
}

