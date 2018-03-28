namespace MazeRunner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Бегущий по лабиринту.
    /// </summary>
    public class MazeRunner
    {
        /// <summary>
        /// Проход по лабиринту по кратчайшему пути.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        /// <param name="entry">
        /// Точка входа в лабиринт.
        /// </param>
        /// <param name="exit">
        /// Точка выхода из лабиринта.
        /// </param>
        /// <returns>
        /// Финальная клетка пути.
        /// </returns>
        public static MazeCell MazeRunLinq(byte[,] maze, MazeCell entry, MazeCell exit)
        {
            var steps = entry.GetSteps(maze);

            while (true)
            {
                var exitCell = steps.FirstOrDefault(s => s.Equals(exit));
                if (exitCell != null)
                {
                    return exitCell;
                }

                steps = steps.SelectMany(s => s.GetSteps(maze)).ToList();

                if (steps.Any())
                {
                    continue;
                }

                return null;
            }
        }

        /// <summary>
        /// Проход по лабиринту по кратчайшему пути.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        /// <param name="entry">
        /// Точка входа в лабиринт.
        /// </param>
        /// <param name="exit">
        /// Точка выхода из лабиринта.
        /// </param>
        /// <returns>
        /// Финальная клетка пути.
        /// </returns>
        public static MazeCell MazeRun(byte[,] maze, MazeCell entry, MazeCell exit)
        {
            var steps = entry.GetSteps(maze);

            while (true)
            {
                var newSteps = new List<MazeCell>();
                foreach (var step in steps)
                {
                    if (step.Equals(exit))
                    {
                        return step;
                    }

                    var cells = step.GetSteps(maze);
                    newSteps.AddRange(cells);
                }

                if (!newSteps.Any())
                {
                    return null;
                }

                steps = newSteps;
            }
        }

        /// <summary>
        /// Напечатать лабиринт.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        public static void PrintMaze(byte[,] maze)
        {
            var m = maze.GetLength(0);
            var n = maze.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($" {maze[i, j]}");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Напечатать путь в лабиринте.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        /// <param name="path">
        /// Путь.
        /// </param>
        public static void PrintPath(byte[,] maze, ICollection<MazeCell> path)
        {
            var color = Console.ForegroundColor;

            var m = maze.GetLength(0);
            var n = maze.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var cell = new MazeCell(i, j);

                    if (path.Contains(cell))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }

                    Console.Write($" {maze[i, j]}");

                    Console.ForegroundColor = color;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создать клон лабиринта.
        /// </summary>
        /// <param name="maze">
        /// Лабиринт.
        /// </param>
        /// <returns>
        /// Клон лабиринта.
        /// </returns>
        public static byte[,] MazeClone(byte[,] maze)
        {
            var m = maze.GetLength(0);
            var n = maze.GetLength(1);
            var mazeClone = new byte[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mazeClone[i, j] = maze[i, j];
                }
            }

            return mazeClone;
        }
    }
}
