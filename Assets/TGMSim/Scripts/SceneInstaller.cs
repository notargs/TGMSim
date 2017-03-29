using UnityEngine;
using Zenject;

namespace TGMSim.Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Material _material;

        public override void InstallBindings()
        {
            Container.Bind<ITetriMinoRotation>().To<TetriMinoRotation>().AsTransient();

            Container.Bind<ITetriMinoSupplier>().To<TetriMinoSupplier>().AsSingle();
            Container.Bind<ITetriMinoRandomizer>().To<TetriMinoRandomizer>().AsSingle();
            Container.Bind<INextPresenter>().To<NextPresenter>().AsSingle();
            Container.Bind<IInputManager>().To<InputManager>().AsSingle();
            Container.Bind<IActiveMino>().To<ActiveMino>().AsSingle();
            Container.Bind<ITetriMinoRepositoryInitializer>().To<TetriMinoRepositoryInitializer>().AsSingle();
            Container.Bind<ITetriMinoRepository>().FromInstance(new TetriMinoRepository()).AsSingle();
            Container.Bind<IField>().To<Field>().AsSingle();

            Container.Bind<ActiveMino>().AsSingle();

            Container.Bind<Material>().FromInstance(_material).AsSingle();
            Container.Bind<IBlockRenderer>().To<BlockRenderer>().AsSingle();

            Container.Bind<MainLoop>().AsSingle();

            Container.Bind<MainLoopMonoBehaviour>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}