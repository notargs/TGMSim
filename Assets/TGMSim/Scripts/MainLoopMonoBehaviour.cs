using UnityEngine;
using Zenject;

namespace TGMSim.Scripts
{
    public class MainLoopMonoBehaviour : MonoBehaviour
    {
        private MainLoop _mainLoop;

        [Inject]
        public void Initialize(MainLoop mainLoop)
        {
            _mainLoop = mainLoop;
        }

        private void Update()
        {
            _mainLoop.Update();
            _mainLoop.Render();
        }
    }
}