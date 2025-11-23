using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.Installers
{
    public class MainMenuInstaller: MonoInstaller
    {
        const string MainMenuUIRootPath = "UI/MainMenu/MainMenuUIRoot";

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresentersFactory>().AsSingle();

            Container.Bind<MainMenuUIRoot>()
                .FromComponentInNewPrefabResource(MainMenuUIRootPath)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<MainMenuScreenPresenter>()
                .FromMethod(CreateMainMenuScreenPresenter)
                .AsSingle()
                .NonLazy();
        }

        private MainMenuScreenPresenter CreateMainMenuScreenPresenter(InjectContext context)
        {
            MainMenuUIRoot uIRoot = Container.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = Container
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uIRoot.HUDLayer);

            MainMenuScreenPresenter presenter = Container
                .Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
        }
    }
}
