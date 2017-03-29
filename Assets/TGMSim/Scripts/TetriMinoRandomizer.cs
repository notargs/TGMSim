using System;
using System.Linq;

namespace TGMSim.Scripts
{
    public class TetriMinoRandomizer : ITetriMinoRandomizer
    {
        private readonly int _tetriMinoCount = Enum.GetNames(typeof(TetriMinoType)).Length;
        private TetriMinoType[] _table;
        private int _tableIterator;

        public TetriMinoRandomizer()
        {
            ResetTable();
        }

        private void ResetTable()
        {
            _tableIterator = 0;
            _table = Enumerable.Range(0, _tetriMinoCount).OrderBy(i => Guid.NewGuid()).Select(v => (TetriMinoType)v).ToArray();
        }
        public TetriMinoType Next()
        {
            if (_tableIterator >= _tetriMinoCount)
            {
                ResetTable();
            }
            var tetriMino = _table[_tableIterator];
            ++_tableIterator;

            return tetriMino;
        }
    }
}