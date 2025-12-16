using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.Installers
{
    public   class GameplayInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind<EntitiesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntitiesLifeContext>().AsSingle();
            Container.BindInterfacesAndSelfTo<MonoEntitiesFactory>().AsSingle().NonLazy();
        }
    }
}
