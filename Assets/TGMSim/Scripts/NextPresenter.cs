namespace TGMSim.Scripts
{
    public class NextPresenter : INextPresenter
    {
        private readonly ITetriMinoRepository _tetriMinoRepository;
        private readonly ITetriMinoSupplier _tetriMinoSupplier;
        private readonly IBlockRenderer _blockRenderer;
        private readonly ITetriMinoRotation _tetriMinoRotation;

        public NextPresenter(ITetriMinoRepository tetriMinoRepository, IBlockRenderer blockRenderer, ITetriMinoRotation tetriMinoRotation, ITetriMinoSupplier tetriMinoSupplier)
        {
            _tetriMinoRepository = tetriMinoRepository;
            _blockRenderer = blockRenderer;
            _tetriMinoRotation = tetriMinoRotation;
            _tetriMinoSupplier = tetriMinoSupplier;
        }

        public void Render()
        {
            var tetriMino = _tetriMinoRepository.GetTetriMino(_tetriMinoSupplier.Next, _tetriMinoRotation);
            var color = tetriMino.Color;
            var mino = tetriMino;
            foreach (var point in mino.Points)
            {
                _blockRenderer.Add(point + new Point(13, 0), color);
            }
        }
    }
}