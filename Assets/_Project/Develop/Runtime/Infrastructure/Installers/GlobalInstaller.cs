using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";
    const string StandartLoadingScreenPath = "Utilities/StandartLoadingScreen";

    public override void InstallBindings()
    {
        Container.Bind<ResourcesAssetsLoader>().AsSingle();

        Container.BindInterfacesTo<CoroutinesPerformer>()
            .FromComponentInNewPrefabResource(CoroutinesPerformerPath)
            .AsSingle();

        Container.BindInterfacesTo<ResourcesConfigsLoader>().AsSingle();

        Container.Bind<ConfigsProviderService>().AsSingle();

        BindLoader();

        Container.BindInterfacesTo<StandartLoadingScreen>()
            .FromComponentsInNewPrefabResource(StandartLoadingScreenPath)
            .AsSingle();


        Container.Bind<SceneSwitcherService>().AsSingle();
    }

    private void BindLoader()
    {
        Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
    }
}
