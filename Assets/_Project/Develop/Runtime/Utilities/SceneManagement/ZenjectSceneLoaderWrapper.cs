using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ZenjectSceneLoaderWrapper
{
    private readonly ZenjectSceneLoader _loader;

    public ZenjectSceneLoaderWrapper(ZenjectSceneLoader loader)
    {
        _loader = loader;
    }

    public IEnumerator LoadAsync(Action<DiContainer> action, string sceneName)
    {
        AsyncOperation wait = _loader.LoadSceneAsync(sceneName, LoadSceneMode.Single, action);
        yield return new WaitWhile(() => wait.isDone == false);
    }
}
