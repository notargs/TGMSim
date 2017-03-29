using System.Collections.Generic;
using UnityEngine;

namespace TGMSim.Scripts
{
    public class TetriMino : ITetriMino
    {
        public TetriMino(bool[,] blocks, Color color, Point center)
        {
            Center = center;
            Blocks = blocks;
            Color = color;

            Points = new List<Point>();
            for (var i = 0; i < blocks.GetLength(0); i++)
            {
                for (var j = 0; j < blocks.GetLength(1); j++)
                {
                    if (Blocks[i, j])
                    {
                        Points.Add(new Point(j, i));
                    }
                }
            }
        }

        public bool[,] Blocks { get; private set; }
        public List<Point> Points { get; private set; }
        public Color Color { get; private set; }
        public Point Center { get; private set; }
    }
}