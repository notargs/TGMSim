using UniRx;
using UnityEngine;

namespace TGMSim.Scripts
{
    public class InputManager : IInputManager
    {
        private readonly Subject<Unit> _onUpdate = new Subject<Unit>();

        public IObservable<Unit> OnRotate1ButtonDown { get; private set; }
        public IObservable<Unit> OnRotate2ButtonDown { get; private set; }
        public IObservable<Unit> OnRightButtonDown { get; private set; }
        public IObservable<Unit> OnLeftButtonDown { get; private set; }
        public IObservable<Unit> OnUpButtonDown { get; private set; }
        public IObservable<Unit> OnDownButtonDown { get; private set; }

        public bool IsRotate1Down { get; private set; }
        public bool IsRotate2Down { get; private set; }

        public InputManager()
        {
            OnRotate1ButtonDown = _onUpdate.Where(_ => Input.GetButtonDown("Rotate1"));
            OnRotate2ButtonDown = _onUpdate.Where(_ => Input.GetButtonDown("Rotate2"));
            OnRightButtonDown = CreateStickObservable("Horizontal", 1);
            OnLeftButtonDown = CreateStickObservable("Horizontal", -1);
            OnUpButtonDown = CreateStickObservable("Vertical", -1);
            OnDownButtonDown = CreateStickObservable("Vertical", 1);
        }

        public IObservable<Unit> CreateButtonObservable(string button)
        {
            var buttonDown = _onUpdate.Where(_ => Input.GetButtonDown(button));
            var buttonUp = _onUpdate.Where(_ => Input.GetButtonUp(button));
            var hold = buttonDown.SelectMany(_onUpdate.Skip(16).TakeUntil(buttonUp));
            return buttonDown.Merge(hold);
        }

        public IObservable<Unit> CreateStickObservable(string stick, int sign)
        {
            var buttonDown = _onUpdate.Select(_ => Input.GetAxis(stick) * sign > 0).DistinctUntilChanged().Where(v => v).Select(_ => Unit.Default);
            var buttonUp = _onUpdate.Select(_ => Input.GetAxis(stick) * sign > 0).DistinctUntilChanged().Where(v => !v).Select(_ => Unit.Default);
            var hold = buttonDown.SelectMany(_onUpdate.Skip(16).TakeUntil(buttonUp));
            return buttonDown.Merge(hold);
        }

        public void Update()
        {
            IsRotate1Down = Input.GetButton("Rotate1");
            IsRotate2Down = Input.GetButton("Rotate2");
            _onUpdate.OnNext(Unit.Default);
        }
    }
}