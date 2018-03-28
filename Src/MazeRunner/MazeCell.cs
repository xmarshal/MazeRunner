namespace MazeRunner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using static StepDirection;

    /// <summary>
    /// Клетка лабиринта.
    /// </summary>
    public class MazeCell : IEquatable<MazeCell>
    {
        /// <summary>
        /// Инициализирует новый экземпляр клетки лабиринта.
        /// </summary>
        /// <param name="x">
        /// Координата по вертикали.
        /// </param>
        /// <param name="y">
        /// Координата по горизонтали.
        /// </param>
        /// <param name="previousStep">
        /// Предыдущий шаг в данном пути
        /// </param>
        /// <param name="allowedDirections">
        /// Допустимые направления движения
        /// </param>
        public MazeCell(int x,
                        int y,
                        [CanBeNull] MazeCell previousStep = null,
                        StepDirection allowedDirections = Up | Down | Left | Right)
        {
            this.X = x;
            this.Y = y;
            this.AllowedDirections = allowedDirections;
            this.PreviousStep = previousStep;
        }

        /// <summary>
        /// Координата по вертикали.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Координата по горизонтали.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Допустимые направления для движения из данной клетки.
        /// </summary>
        public StepDirection AllowedDirections { get; }

        /// <summary>
        /// Предыдущий шаг в данном пути.
        /// </summary>
        public MazeCell PreviousStep { get; }

        /// <summary>
        /// Получить строковое представление пройденного пути.
        /// </summary>
        /// <returns>
        /// Строковое представление пройденного пути.
        /// </returns>
        public string GetPathString()
        {
            var cells = this.GetPath();
            return string.Join(";", cells);
        }

        /// <summary>
        /// Получить список клеток в пути.
        /// </summary>
        /// <returns>
        /// Список клеток в пути.
        /// </returns>
        public List<MazeCell> GetPath()
        {
            var cells = new List<MazeCell> { this };
            var p = this.PreviousStep;
            while (p != null)
            {
                cells.Add(p);
                p = p.PreviousStep;
            }

            cells.Reverse();
            return cells;
        }

        /// <summary>
        /// Получить возможные шаги из данной клетки.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт по которому осуществляется движение.
        /// </param>
        /// <returns>
        /// Список клеток в которые возможно осуществить шаг из данной.
        /// </returns>
        public List<MazeCell> GetSteps(byte[,] maze)
        {
            var steps = new[]
                {
                    this.StepTo(maze, Right),
                    this.StepTo(maze, Down),
                    this.StepTo(maze, Left),
                    this.StepTo(maze, Up)
                };

            var cells = steps.Where(s => s != null).ToList();

            maze[this.X, this.Y] = 1;

            return cells;
        }

        /// <summary>
        /// Осуществить шаг в выбранном направлении в лабиринте.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        /// <param name="direction">
        /// Выбранное направление движения.
        /// </param>
        /// <returns>
        /// В случае возможности шага в выбранном направлении возвращает новый экземпляр клетки лабиринта иначе null
        /// </returns>
        [CanBeNull]
        public MazeCell StepTo(byte[,] maze, StepDirection direction)
        {
            if ((this.AllowedDirections & direction) == 0)
            {
                return null;
            }

            var x = this.X;
            var y = this.Y;

            var inRange = false;
            switch (direction)
            {
                case Up:
                    x -= 1;
                    inRange = x > 0;
                    break;
                case Down:
                    x += 1;
                    inRange = x < maze.GetLength(0);
                    break;
                case Left:
                    y -= 1;
                    inRange = y > 0;
                    break;
                case Right:
                    y += 1;
                    inRange = y < maze.GetLength(1);
                    break;
            }

            if (inRange && maze[x, y] == 0)
            {
                return new MazeCell(x, y, this, ~direction | direction);
            }

            return null;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.X:D2},{this.Y:D2}";
        }

        /// <inheritdoc />
        public bool Equals(MazeCell other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.X == other.X && this.Y == other.Y;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other is MazeCell point && this.Equals(point);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }
    }
}