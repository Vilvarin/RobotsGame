using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инструмент сборщика. Позволяет собрать находящиеся вокруг ресурсы
    /// </summary>
    public class CollectInstrument : MonoBehaviour, IInstrument
    {
        Transform _transform;

        #region properties

        InstrumentsEnum IInstrument.InstrumentCode
        {
            get { return InstrumentsEnum.Collect; }
        }

        public static readonly string instrumentName = "собиратель";
        string IInstrument.InstrumentName
        {
            get { return instrumentName; }
        }

        #endregion

        void Start()
        {
            _transform = GetComponent<Transform>();
            GetComponent<Robot>().SwitchInstrument();
        }

        /// <summary>
        /// Использовать инструмент
        /// </summary>
        /// <param name="direction">Направление движения шасси</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator Use(bool direction)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Collected block;

                    if (BoardModel.GetBlock<Collected>(_transform.position + new Vector3(x, y, 0), out block))
                    {
                        block.Collect();
                    }
                }
            }

            yield return null;
        }
    }
}

