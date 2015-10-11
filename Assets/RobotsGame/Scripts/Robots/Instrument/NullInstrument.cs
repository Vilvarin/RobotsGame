using UnityEngine;
using System.Collections;

namespace RobotsGame
{
    /// <summary>
    /// Инструмент по умолчанию
    /// </summary>
    public class NullInstrument : MonoBehaviour, IInstrument
    {

        public InstrumentsEnum InstrumentCode
        {
            get { return InstrumentsEnum.None; }
        }

        public static readonly string instrumentName = " ";
        public string InstrumentName
        {
            get { return instrumentName; }
        }


        public IEnumerator Use(bool direction)
        {
            throw new System.NotImplementedException();
        }
    }
}

