using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}