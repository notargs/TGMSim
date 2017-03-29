namespace TGMSim.Scripts
{
    public struct Point
    {
        public Point(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }
}