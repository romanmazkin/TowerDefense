using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        ICoroutinesPerformer _coroutinesPerformer;
        ConfigsProviderService _configProviderService;

        [Inject]
        private void Construct(
            ICoroutinesPerformer coroutinesPerformer, 
            ConfigsProviderService configsProviderService)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _configProviderService = configsProviderService;
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

            Debug.Log("Start services initialization");

            yield return _configProviderService.LoadAsync();

            yield return new WaitForSeconds(1f);

            Debug.Log("End services initialization");

            Debug.Log("Close loading screen");

            Debug.Log("Switch scene");
        }
    }
}