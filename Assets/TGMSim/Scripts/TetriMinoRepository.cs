using System.Collections.Generic;
using UnityEngine;

namespace TGMSim.Scripts
{
    public interface ITetriMinoRepositoryInitializer
    {
        void Initialize();
    }

    public class TetriMinoRepositoryInitializer : ITetriMinoRepositoryInitializer
    {
        private readonly ITetriMinoRepository _tetriMinoRepository;

        public TetriMinoRepositoryInitializer(ITetriMinoRepository tetriMinoRepository)
        {
            _tetriMinoRepository = tetriMinoRepository;
        }

        public void Initialize()
        {
            var orange = new Color(1, 0.5f, 0);
            _tetriMinoRepository.MinoData = new Dictionary<TetriMinoType, ITetriMino[]>
            {
                {
                    TetriMinoType.T, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, false, false},
                            {true, true, true},
                            {false, true, false},
                        }, Color.cyan, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, false},
                            {false, true, true},
                            {false, true, false},
                        }, Color.cyan, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, false},
                            {true, true, true},
                        }, Color.cyan, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, false},
                            {true, true, false},
                            {false, true, false},
                        }, Color.cyan, new Point(1, 1)),
                    }
                },
                {
                    TetriMinoType.J, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, false, false},
                            {true, true, true},
                            {false, false, true},
                        }, Color.blue, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, true},
                            {false, true, false},
                            {false, true, false},
                        }, Color.blue, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {true, false, false},
                            {true, true, true},
                        }, Color.blue, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, false},
                            {false, true, false},
                            {true, true, false},
                        }, Color.blue, new Point(1, 1)),
                    }
                },
                {
                    TetriMinoType.L, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, false, false},
                            {true, true, true},
                            {true, false, false},
                        }, orange, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, true, false},
                            {false, true, false},
                            {false, true, true},
                        }, orange, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, false, true},
                            {true, true, true},
                        }, orange, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {true, true, false},
                            {false, true, false},
                            {false, true, false},
                        }, orange, new Point(1, 1)),
                    }
                },
                {
                    TetriMinoType.Z, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {true, true, false},
                            {false, true, true},
                        }, Color.green, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {false, false, true},
                            {false, true, true},
                            {false, true, false},
                        }, Color.green, new Point(1, 1)),
                    }
                },
                {
                    TetriMinoType.S, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, true, true},
                            {true, true, false},
                        }, Color.magenta, new Point(1, 1)),
                        new TetriMino(new[,]
                        {
                            {true, false, false},
                            {true, true, false},
                            {false, true, false},
                        }, Color.magenta, new Point(1, 1)),
                    }
                },
                {
                    TetriMinoType.I, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, false, false, false},
                            {true, true, true, true},
                        }, Color.red, new Point(2, 1)),
                        new TetriMino(new[,]
                        {
                            {false, false, true, false},
                            {false, false, true, false},
                            {false, false, true, false},
                            {false, false, true, false},
                        }, Color.red, new Point(2, 1)),
                    }
                },
                {
                    TetriMinoType.O, new ITetriMino[]
                    {
                        new TetriMino(new[,]
                        {
                            {false, true, true},
                            {false, true, true},
                        }, Color.yellow, new Point(1, 1)),
                    }
                },
            };
        }
    }

    public class TetriMinoRepository : ITetriMinoRepository
    {
        public Dictionary<TetriMinoType, ITetriMino[]> MinoData { get; set; }

        private static int Mod(int a, int b)
        {
            var ret = a % b;
            if (ret < 0) ret += b;
            return ret;
        }
        
        public ITetriMino GetTetriMino(TetriMinoType tetriMinoType, ITetriMinoRotation tetriMinoRotation)
        {
            var minos = MinoData[tetriMinoType];
            return minos[Mod(tetriMinoRotation.Id, minos.Length)];
        }
    }
}