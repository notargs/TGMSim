namespace TGMSim.Scripts
{
    public class TetriMinoSupplier : ITetriMinoSupplier
    {
        private readonly ITetriMinoRandomizer _tetriMinoRandomizer;

        public TetriMinoSupplier(ITetriMinoRandomizer tetriMinoRandomizer)
        {
            _tetriMinoRandomizer = tetriMinoRandomizer;
            Next = _tetriMinoRandomizer.Next();
            Current = _tetriMinoRandomizer.Next();
        }

        public void Update()
        {
            Current = Next;
            Next = _tetriMinoRandomizer.Next();
        }
        public TetriMinoType Current { get; private set; }
        public TetriMinoType Next { get; private set; }
    }
}