using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private bool _isRunning;
        private EntitiesFactory _entitiesFactory;

        [Inject]
        public void Construct(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void Initialize()
        {
        }

        public void Run()
        {
            Entity entity = _entitiesFactory.CreateTestEntity();

            Debug.Log("Move direction " + entity.GetComponent<MoveDirection>().Value.Value.ToString());
            Debug.Log("Move speed " + entity.GetComponent<MoveSpeed>().Value.Value.ToString());

            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;
        }
    }
}
