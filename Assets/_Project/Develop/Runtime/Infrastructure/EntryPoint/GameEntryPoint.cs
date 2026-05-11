using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        ICoroutinesPerformer _coroutinesPerformer;
        ConfigsProviderService _configProviderService;
        ILoadingScreen _loadingScreen;
        SceneSwitcherService _sceneSwitcherService;
        PlayerDataProvider _playerDataProvider;
        WalletService _walletService;

        [Inject]
        public void Construct(
            ICoroutinesPerformer coroutinesPerformer,
            ConfigsProviderService configsProviderService,
            ILoadingScreen standartLoadingScreen,
            SceneSwitcherService sceneSwitcherService,
            PlayerDataProvider playerDataProvider,
            WalletService walletService)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _configProviderService = configsProviderService;
            _loadingScreen = standartLoadingScreen;
            _sceneSwitcherService = sceneSwitcherService;
            _playerDataProvider = playerDataProvider;
            _walletService = walletService;
        }

        private void Awake()
        {
            Debug.Log("Project start");

            SetupAppSettings();

            _coroutinesPerformer.StartPerform(Initialize());
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize()
        {
            Debug.Log("Open loading screen");

            _loadingScreen.Show();

            Debug.Log("Start services initialization");

            yield return _configProviderService.LoadAsync();

            bool isPlayerDataSaveExists = false;

            yield return _playerDataProvider.ExistsAsync(result => isPlayerDataSaveExists = result);

            if (isPlayerDataSaveExists)
                yield return _playerDataProvider.LoadAsync();
            else
                _playerDataProvider.Reset();

            yield return new WaitForSeconds(1f);

            Debug.Log("End services initialization");

            Debug.Log("Close loading screen");

            _loadingScreen.Hide();

            Debug.Log("Switch scene");

            yield return _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}