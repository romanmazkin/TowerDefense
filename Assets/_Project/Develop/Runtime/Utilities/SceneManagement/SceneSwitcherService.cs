using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System.Collections;

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

        public IEnumerator ProcessSwitchTo(string sceneName)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(Scenes.Empty);
            yield return _sceneLoader.LoadAsync(sceneName);

            _loadingScreen.Hide();
        }
    }
}
