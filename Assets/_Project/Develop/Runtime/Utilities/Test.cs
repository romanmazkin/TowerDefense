using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Utilities
{
    public class Test : MonoBehaviour
    {
        const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";

        private ICoroutinesPerformer _coroutinesPerformer;
        private ResourcesAssetsLoader _resourcesAssetsLoader;
        private ConfigsProviderService _configsProviderService;

        [Inject]
        private void Construct(
            ICoroutinesPerformer coroutinesPerformer,
            ResourcesAssetsLoader resourcesAssetsLoader,
            ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;
            _coroutinesPerformer = coroutinesPerformer;
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        private void Awake()
        {
            //_resourcesAssetsLoader = CreateResourcesAssetsLoader();

            //_coroutinesPerformer = CreateCoroutinesPerformer();
            //_configsProviderService = CreateConfigsProviderService();

            //ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(_resourcesAssetsLoader);

            _coroutinesPerformer.StartPerform(LoadConfigs());


        }

        //private ConfigsProviderService CreateConfigsProviderService()
        //{
        //    ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(_resourcesAssetsLoader);

        //    return new ConfigsProviderService(resourcesConfigsLoader);
        //}

        //private ResourcesAssetsLoader CreateResourcesAssetsLoader() => new ResourcesAssetsLoader();

        //private CoroutinesPerformer CreateCoroutinesPerformer()
        //{
        //    CoroutinesPerformer CoroutinesPerformerPrefab = _resourcesAssetsLoader
        //        .Load<CoroutinesPerformer>(CoroutinesPerformerPath);

        //    return Instantiate(CoroutinesPerformerPrefab);
        //}

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                TestConfig config = _configsProviderService.GetConfig<TestConfig>();
                Debug.Log(config.Damage);
            }
        }

        private IEnumerator LoadConfigs()
        {
            Debug.Log("StartLoadConfigs");
            yield return _configsProviderService.LoadAsync();
            Debug.Log("EndLoadConfigs");
        }
    }
}