using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EntitiesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntitiesLifeContext>().AsSingle();
            Container.Bind<CollidersRegisteryService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MonoEntitiesFactory>().AsSingle().NonLazy();
            Container.Bind<BrainsFactory>().AsSingle();
            Container.Bind<MainHeroFactory>().AsSingle();
            Container.Bind<EnemiesFactory>().AsSingle();
            Container.Bind<AIBrainsContext>().AsSingle();
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        }
    }
}
