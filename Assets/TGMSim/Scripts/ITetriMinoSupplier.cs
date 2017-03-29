namespace TGMSim.Scripts
{
    public interface ITetriMinoSupplier
    {
        void Update();
        TetriMinoType Current { get; }
        TetriMinoType Next { get; }
    }
}