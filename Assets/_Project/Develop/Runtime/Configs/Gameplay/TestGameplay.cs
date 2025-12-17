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

        private Entity _entity;

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
            _entity = _entitiesFactory.CreateTestEntity(Vector3.zero);

            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _entity.GetComponent<MoveDirection>().Value.Value = input;
        }
    }
}
