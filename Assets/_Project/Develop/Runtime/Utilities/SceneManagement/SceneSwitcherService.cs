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
        ILevelLoader _levelLoader;
        ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            ISceneLoader sceneLoader, 
            ILevelLoader levelLoader, 
            ILoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _levelLoader = levelLoader;
            _loadingScreen = loadingScreen;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, LevelLoadingData levelLoadingData = null)
        {
            _loadingScreen.Show();

            //yield return _sceneLoader.LoadAsync(Scenes.Empty);

            if (sceneName == Scenes.Gameplay)
                yield return _levelLoader.LoadAsync(levelLoadingData);
            else
                yield return _sceneLoader.LoadAsync(sceneName);

            if (levelLoadingData != null)
                Debug.Log($"Loaded scene {sceneName} with level {levelLoadingData.Level}");
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