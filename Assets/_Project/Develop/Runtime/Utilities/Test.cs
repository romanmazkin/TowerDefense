using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";

    private ICoroutinesPerformer _coroutinesPerformer;
    private ResourcesAssetsLoader _resourcesAssetsLoader;
    private ConfigsProviderService _configsProviderService;

    private void Awake()
    {
        _resourcesAssetsLoader = CreateResourcesAssetsLoader();

        _coroutinesPerformer = CreateCoroutinesPerformer();

        _configsProviderService = CreateConfigsProviderService();

        _coroutinesPerformer.StartPerform(LoadConfigs());
    }

    private ConfigsProviderService CreateConfigsProviderService()
    {
        ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(_resourcesAssetsLoader);

        return new ConfigsProviderService(resourcesConfigsLoader);
    }

    private ResourcesAssetsLoader CreateResourcesAssetsLoader() => new ResourcesAssetsLoader();

    private CoroutinesPerformer CreateCoroutinesPerformer()
    {
        CoroutinesPerformer CoroutinesPerformerPrefab = _resourcesAssetsLoader
            .Load<CoroutinesPerformer>(CoroutinesPerformerPath);

        return Instantiate(CoroutinesPerformerPrefab);
    }

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
