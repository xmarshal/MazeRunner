namespace MazeRunner
{
    using System;

    internal class GraphEdge : IEquatable<GraphEdge>
    {
        public (int X, int Y)[] Vertices { get; } = new (int X, int Y)[2];

        public double Power { get; set; }

        /// <inheritdoc />
        public bool Equals(GraphEdge other)
        {
            return (this.Vertices[0].X == other.Vertices[0].X && this.Vertices[0].Y == other.Vertices[0].Y && 
                    this.Vertices[1].X == other.Vertices[1].X && this.Vertices[1].Y == other.Vertices[1].Y)
                   || (this.Vertices[0].X == other.Vertices[1].X && this.Vertices[0].Y == other.Vertices[1].Y && 
                       this.Vertices[1].X == other.Vertices[0].X && this.Vertices[1].Y == other.Vertices[0].Y);
        }
    }
}