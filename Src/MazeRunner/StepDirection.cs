namespace MazeRunner
{
    using System;

    /// <summary>
    /// Направление шага в лабиринте
    /// </summary>
    [Flags]
    public enum StepDirection
    {
        /// <summary>
        /// Направление не задано
        /// </summary>
        None = 0,

        /// <summary>
        /// Направление вверх
        /// </summary>
        Up = 1,

        /// <summary>
        /// Направлене вниз
        /// </summary>
        Down = 2,

        /// <summary>
        /// Направление налево
        /// </summary>
        Left = 4,

        /// <summary>
        /// Направление направо
        /// </summary>
        Right = 8,

        /// <summary>
        /// The all.
        /// </summary>
        All = Up | Down | Right | Left
    }
}
