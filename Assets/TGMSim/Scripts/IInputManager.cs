using UniRx;

namespace TGMSim.Scripts
{
    public interface IInputManager
    {
        IObservable<Unit> OnRotate1ButtonDown { get; }
        IObservable<Unit> OnRotate2ButtonDown { get; }
        IObservable<Unit> OnRightButtonDown { get; }
        IObservable<Unit> OnLeftButtonDown { get; }
        IObservable<Unit> OnUpButtonDown { get; }
        IObservable<Unit> OnDownButtonDown { get; }

        bool IsRotate1Down { get; }
        bool IsRotate2Down { get; }

        void Update();
    }
}