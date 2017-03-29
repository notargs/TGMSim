using System;
using UnityEngine;

namespace TGMSim.Scripts
{
    [Serializable]
    public class MainLoop
    {
        private readonly ITetriMinoRepositoryInitializer _tetriMinoRepositoryInitializer;
        private readonly INextPresenter _nextPresenter;
        private readonly IInputManager _inputManager;
        private readonly IBlockRenderer _blockRenderer;
        private readonly IActiveMino _activeMino;
        private readonly IField _field;

        public MainLoop(IField field, IBlockRenderer blockRenderer, IActiveMino activeMino, IInputManager inputManager, INextPresenter nextPresenter, ITetriMinoRepositoryInitializer tetriMinoRepositoryInitializer)
        {
            _blockRenderer = blockRenderer;
            _activeMino = activeMino;
            _inputManager = inputManager;
            _nextPresenter = nextPresenter;
            _tetriMinoRepositoryInitializer = tetriMinoRepositoryInitializer;
            _field = field;

            Application.targetFrameRate = 60;

            _tetriMinoRepositoryInitializer.Initialize();
        }

        public void Update()
        {
            _inputManager.Update();
            _activeMino.Update();
        }

        public void Render()
        {
            _activeMino.Render();
            _field.Render();
            _nextPresenter.Render();

            _blockRenderer.Render();
        }
    }
}