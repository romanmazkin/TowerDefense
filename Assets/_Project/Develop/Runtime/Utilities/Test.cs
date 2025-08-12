using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";

    private ICoroutinesPerformer _coroutinesPerformer;
    private ResourcesAssetsLoader _resourcesAssetsLoader;

    private void Awake()
    {
        _resourcesAssetsLoader = CreateResourcesAssetsLoader();

        _coroutinesPerformer = CreateCoroutinesPerformer();
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
        if (Input.GetKeyDown(KeyCode.F))
            _coroutinesPerformer.StartPerform(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(1);
        Debug.Log("End");
    }
}
