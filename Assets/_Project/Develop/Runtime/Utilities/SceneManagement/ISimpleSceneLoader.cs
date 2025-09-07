using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public interface ISimpleSceneLoader
    {
        public IEnumerator LoadAsync(string sceneName); 
    }
}
