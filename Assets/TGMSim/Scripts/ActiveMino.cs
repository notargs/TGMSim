using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace TGMSim.Scripts
{
    public class ActiveMino : IActiveMino
    {
        private readonly IBlockRenderer _blockRenderer;
        private readonly ITetriMinoRepository _tetriMinoRepository;
        private readonly Subject<Unit> _onUpdateSubject = new Subject<Unit>();

        private Point _position;
        private TetriMinoType _minoType;
        private ITetriMinoRotation _rotation;
        private readonly IField _field;

        public ActiveMino(IBlockRenderer blockRenderer, IField field, ITetriMinoRepository tetriMinoRepository,
            IInputManager inputManager, ITetriMinoRotation rotation, ITetriMinoSupplier tetriMinoSupplier)
        {
            _blockRenderer = blockRenderer;
            _tetriMinoRepository = tetriMinoRepository;
            _rotation = rotation;
            _field = field;

            _minoType = tetriMinoSupplier.Current;

            inputManager.OnRotate1ButtonDown.Subscribe(_ => Rotate(rot => rot.RotateRight()));
            inputManager.OnRotate2ButtonDown.Subscribe(_ => Rotate(rot => rot.RotateLeft()));

            inputManager.OnRightButtonDown.Subscribe(_ =>
            {
                if (IsValid(_position + new Point(1, 0), _rotation))
                {
                    _position.X += 1;
                }
            });
            inputManager.OnLeftButtonDown.Subscribe(_ =>
            {
                if (IsValid(_position + new Point(-1, 0), _rotation))
                {
                    _position.X -= 1;
                }
            });

            inputManager.OnDownButtonDown.Subscribe(_ =>
            {
                if (IsValid(_position + new Point(0, 1), _rotation))
                {
                    _position.Y += 1;
                }
                else
                {
                    var tetriMino = _tetriMinoRepository.GetTetriMino(_minoType, _rotation);
                    field.SetTetriMino(tetriMino, _position);
                    field.RemoveLines();

                    tetriMinoSupplier.Update();

                    _position = new Point(3, 0);
                    _minoType = tetriMinoSupplier.Current;
                    _rotation = new TetriMinoRotation();
                    if (inputManager.IsRotate1Down)
                    {
                        Debug.Log("Test");
                        _rotation = _rotation.RotateRight();
                    }
                    else
                    if (inputManager.IsRotate2Down)
                    {
                        _rotation = _rotation.RotateLeft();
                    }
                }
            });

            inputManager.OnUpButtonDown.Subscribe(_ =>
            {
                while (IsValid(_position + new Point(0, 1), _rotation))
                {
                    _position.Y += 1;
                }
            });

            _onUpdateSubject.SelectMany(Observable.Range(0, 20)).Subscribe(_ =>
            {
                if (IsValid(_position + new Point(0, 1), _rotation))
                {
                    _position.Y += 1;
                }
            });
        }

        private void Rotate(Func<ITetriMinoRotation, ITetriMinoRotation> rotateFunc)
        {
            var nextPosition = _position;
            var nextRotation = rotateFunc(_rotation);

            var prevTetriMino = _tetriMinoRepository.GetTetriMino(_minoType, _rotation);
            var prevBottom = prevTetriMino.Blocks.GetLength(0);

            var nextTetriMino = _tetriMinoRepository.GetTetriMino(_minoType, nextRotation);
            var nextBottom = nextTetriMino.Blocks.GetLength(0);

            nextPosition.Y += Mathf.Max(0, prevBottom - nextBottom);

            if (IsValid(nextPosition, nextRotation))
            {
                _position = nextPosition;
                _rotation = rotateFunc(_rotation);
            }
            else if (CanKick(nextPosition, nextRotation))
            {
                if (IsValid(nextPosition + new Point(1, 0), nextRotation))
                {
                    nextPosition.X += 1;
                    _position = nextPosition;
                    _rotation = rotateFunc(_rotation);
                }
                else if (IsValid(nextPosition + new Point(-1, 0), nextRotation))
                {
                    nextPosition.X -= 1;
                    _position = nextPosition;
                    _rotation = rotateFunc(_rotation);
                }
            }
        }

        private bool IsValid(Point position, ITetriMinoRotation rotation)
        {
            var tetriMino = _tetriMinoRepository.GetTetriMino(_minoType, rotation);
            return tetriMino.Points.Count(point => _field.IsFull(point + position)) == 0;
        }

        private bool CanKick(Point position, ITetriMinoRotation rotation)
        {
            var tetriMino = _tetriMinoRepository.GetTetriMino(_minoType, rotation);
            return tetriMino.Points.Where(p => p.X == tetriMino.Center.X).Count(point => _field.IsFull(point + position)) == 0;
        }

        public void Update()
        {
            _onUpdateSubject.OnNext(Unit.Default);
        }

        public void Render()
        {
            var tetriMino = _tetriMinoRepository.GetTetriMino(_minoType, _rotation);
            var color = tetriMino.Color;
            var mino = tetriMino;
            foreach (var point in mino.Points)
            {
                var pos = point + _position;
                _blockRenderer.Add(pos, color);
            }
        }
    }
}