using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public interface ILevelLoader
    {
        IEnumerator LoadAsync(LevelLoadingData levelLoadingData);
    }
}
