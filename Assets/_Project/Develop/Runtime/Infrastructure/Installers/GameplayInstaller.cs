using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.Installers
{
    public   class GameplayInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind<EntitiesFactory>().AsSingle();
        }
    }
}
