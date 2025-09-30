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
            Container.Bind<MainMenuUIRoot>()
                .FromComponentInNewPrefabResource(MainMenuUIRootPath)
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPresentersFactory>().AsSingle();

            Container.Bind<MainMenuScreenPresenter>().FromMethod(CreateMainMenuScreenPresenter).AsSingle().NonLazy();
        }

        private MainMenuScreenPresenter CreateMainMenuScreenPresenter()
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
