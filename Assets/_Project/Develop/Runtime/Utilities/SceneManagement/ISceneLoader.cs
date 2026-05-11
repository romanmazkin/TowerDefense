using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public interface ISceneLoader
    {
        public IEnumerator LoadAsync(string sceneName);
        public IEnumerator LoadAsync(string sceneName, SceneLoadingData sceneLoadingData);
    }
}
