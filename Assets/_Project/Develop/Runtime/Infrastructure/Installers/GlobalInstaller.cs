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

        Container.BindInterfacesAndSelfTo<CoroutinesPerformer>().
            FromComponentInNewPrefabResource(CoroutinesPerformerPath).
            AsSingle();

        Container.BindInterfacesAndSelfTo<ResourcesConfigsLoader>().AsSingle();

        Container.Bind<ConfigsProviderService>().AsSingle();

        Container.Bind<SceneLoaderService>().AsSingle();

        Container.BindInterfacesAndSelfTo<StandartLoadingScreen>().
            FromComponentsInNewPrefabResource(StandartLoadingScreenPath).
            AsSingle();
    }
}
