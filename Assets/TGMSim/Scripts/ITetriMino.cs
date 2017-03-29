using System.Collections.Generic;
using UnityEngine;

namespace TGMSim.Scripts
{
    public interface ITetriMino
    {
        bool[,] Blocks { get; }
        List<Point> Points { get; }
        Color Color { get; }
        Point Center { get; }
    }
}