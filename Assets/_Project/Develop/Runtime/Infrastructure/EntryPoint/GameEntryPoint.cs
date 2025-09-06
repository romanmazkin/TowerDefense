using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
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

        [Inject]
        private void Construct(
            ICoroutinesPerformer coroutinesPerformer,
            ConfigsProviderService configsProviderService,
            ILoadingScreen standartLoadingScreen)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _configProviderService = configsProviderService;
            _loadingScreen = standartLoadingScreen;
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

            yield return new WaitForSeconds(1f);

            Debug.Log("End services initialization");

            Debug.Log("Close loading screen");

            _loadingScreen.Hide();

            Debug.Log("Switch scene");
        }
    }
}