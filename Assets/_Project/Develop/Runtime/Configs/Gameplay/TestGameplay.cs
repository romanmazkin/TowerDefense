using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private bool _isRunning;

        [Inject]
        public void Construct()
        {
        }

        public void Initialize()
        {

        }

        public void Run()
        {
            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;
        }
    }
}
