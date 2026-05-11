using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        ISceneLoader _sceneLoader;
        ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            ISceneLoader sceneLoader, 
            ILoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, SceneLoadingData sceneLoadingData = null)
        {
            _loadingScreen.Show();

            //yield return _sceneLoader.LoadAsync(Scenes.Empty);

            if (sceneName == Scenes.Gameplay)
                yield return _sceneLoader.LoadAsync(sceneName, sceneLoadingData);
            else
                yield return _sceneLoader.LoadAsync(sceneName);

            if (sceneLoadingData != null)
                Debug.Log($"Loaded scene {sceneName} with level {sceneLoadingData.Level}");
            else
                Debug.Log($"Loaded scene {sceneName} without levels");


            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(SceneBootstrap) + " not found");

            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}