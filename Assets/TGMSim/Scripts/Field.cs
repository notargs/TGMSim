using UnityEngine;

namespace TGMSim.Scripts
{
    public class Field : IField
    {
        private readonly IBlockRenderer _blockRenderer;

        private readonly Color?[,] _blocks;

        public Field(IBlockRenderer blockRenderer)
        {
            _blockRenderer = blockRenderer;
            _blocks = new Color?[Width, Height];
        }

        public bool IsEmpty(Point point)
        {
            if (point.X < 0)
            {
                return false;
            }

            if (point.X > Width - 1)
            {
                return false;
            }

            if (point.Y < 0)
            {
                return false;
            }

            if (point.Y > Height - 1)
            {
                return false;
            }

            if (_blocks[point.X, point.Y].HasValue)
            {
                return false;
            }

            return true;
        }

        public bool IsFull(Point point)
        {
            return !IsEmpty(point);
        }

        public int Width { get { return 10; } }
        public int Height { get { return 20; } }

        public void Render()
        {
            for (var i = 0; i < Height + 1; ++i)
            {
                _blockRenderer.Add(new Point(-1, i), Color.gray);
                _blockRenderer.Add(new Point(Width, i), Color.gray);
            }

            for (var i = 0; i < Width; ++i)
            {
                _blockRenderer.Add(new Point(i, Height), Color.gray);
            }

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (_blocks[j, i].HasValue)
                    {
                        _blockRenderer.Add(new Point(j, i), _blocks[j, i].Value);
                    }
                }
            }
        }

        public void RemoveLines()
        {
            for (var y = Height - 1; y >= 0; y--)
            {
                var filled = true;
                for (var x = 0; x < Width; x++)
                {
                    if (!_blocks[x, y].HasValue)
                    {
                        filled = false;
                    }
                }
                if (!filled)
                {
                    continue;
                }
                for (var y2 = y; y2 >= 0; y2--)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        if (y2 == 0)
                        {
                            _blocks[x, y2] = null;
                        }
                        else
                        {
                            _blocks[x, y2] = _blocks[x, y2 - 1];
                        }
                    }
                }
                y++;
            }
        }

        public void SetTetriMino(ITetriMino tetriMino, Point position)
        {
            foreach (var point in tetriMino.Points)
            {
                var pos = point + position;
                if (IsFull(pos)) continue;
                _blocks[pos.X, pos.Y] = tetriMino.Color;
            }
        }
    }
}