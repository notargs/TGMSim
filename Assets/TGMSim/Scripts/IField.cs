namespace TGMSim.Scripts
{
    public interface IField
    {
        bool IsEmpty(Point point);
        bool IsFull(Point point);
        int Width { get; }
        int Height { get; }
        void SetTetriMino(ITetriMino tetriMino, Point point);
        void Render();
        void RemoveLines();
    }
}