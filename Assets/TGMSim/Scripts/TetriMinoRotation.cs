namespace TGMSim.Scripts
{
    public struct TetriMinoRotation : ITetriMinoRotation
    {
        public TetriMinoRotation(int id = 0) : this()
        {
            Id = id;
        }

        public int Id { get; private set; }
        
        public ITetriMinoRotation RotateRight()
        {
            return new TetriMinoRotation(Id + 1);
        }

        public ITetriMinoRotation RotateLeft()
        {
            return new TetriMinoRotation(Id - 1);
        }
    }
}