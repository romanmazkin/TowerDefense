using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public class SceneLoaderService : ISceneLoader, ILevelLoader
    {
        private readonly ZenjectSceneLoaderWrapper _sceneLoaderWrapper;

        public SceneLoaderService(ZenjectSceneLoaderWrapper sceneLoader)
        {
            _sceneLoaderWrapper = sceneLoader;
        }

        public IEnumerator LoadAsync(string sceneName)
        {
            if (sceneName == Scenes.Gameplay)
                throw new ArgumentException("gameplay scene load error");

            yield return _sceneLoaderWrapper.LoadAsync(null, sceneName);
        }

        public IEnumerator LoadAsync(LevelLoadingData levelLoadingData)
        {
            yield return _sceneLoaderWrapper.LoadAsync(container =>
            {
                container.BindInstance(levelLoadingData);
            }, Scenes.Gameplay);
        }
    }
}