using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.Serializers;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
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

        Container.Bind<WalletService>().FromMethod(CreateWalletService).AsSingle();

        Container.Bind<ISaveLoadService>().To<SaveLoadService>().FromMethod(CreateSaveLoadService).AsSingle();

        Container.Bind<PlayerDataProvider>().AsSingle();
    }

    private void BindLoader()
    {
        Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
    }

    private static WalletService CreateWalletService()
    {
        Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

        foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
            currencies[currencyType] = new ReactiveVariable<int>();

        return new WalletService(currencies);
    }

    private static SaveLoadService CreateSaveLoadService()
    {
        IDataSerializer dataSerializer = new JsonSerializer();
        IDataKeysStorage dataKeysStorage = new MapDataKeysStorage();

        string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

        IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

        return new SaveLoadService(dataSerializer, dataKeysStorage, dataRepository);
    }
}
