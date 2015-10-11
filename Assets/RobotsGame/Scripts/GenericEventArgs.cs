using System;

namespace RobotsGame
{
    /// <summary>
    /// Аргумент события с одним параметром.
    /// </summary>
    /// <remarks>Нужен чтобы лишний раз не создавать простой класс</remarks>
    /// <typeparam name="T">Передаваемое событием значение</typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        /// <summary>Параметр события</summary>
        public T Value { get; private set; }

        public GenericEventArgs(T value)
        {
            this.Value = value;
        }
    }
}
