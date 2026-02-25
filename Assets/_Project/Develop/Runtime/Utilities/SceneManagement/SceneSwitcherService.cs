using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        SceneLoaderService _sceneLoader;
        ILoadingScreen _loadingScreen;

        public SceneSwitcherService(SceneLoaderService sceneLoader, ILoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, SceneLoadingData sceneLoadingData = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(Scenes.Empty);
            yield return _sceneLoader.LoadAsync(sceneName, sceneLoadingData);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(SceneBootstrap) + " not found");

            yield return sceneBootstrap.Initialize(/*sceneLoadingData*/);

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}
