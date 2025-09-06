using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";

    public override void InstallBindings()
    {
        Container.Bind<ResourcesAssetsLoader>().AsSingle();

        Container.Bind<ICoroutinesPerformer>().To<CoroutinesPerformer>().
            FromComponentInNewPrefabResource(CoroutinesPerformerPath).AsSingle();

        Container.Bind<IConfigsLoader>().To<ResourcesConfigsLoader>().AsSingle();
        Container.Bind<ConfigsProviderService>().AsSingle();
        Container.Bind<SceneLoaderService>().AsSingle();
    }
}
