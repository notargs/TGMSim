using System.Collections.Generic;

namespace TGMSim.Scripts
{
    public interface ITetriMinoRepository
    {
        Dictionary<TetriMinoType, ITetriMino[]> MinoData { get; set; }
        ITetriMino GetTetriMino(TetriMinoType tetriMinoType, ITetriMinoRotation tetriMinoRotation);
    }
}