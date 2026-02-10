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
            _entity = _entitiesFactory.CreateHero(Vector3.zero);
            _entitiesFactory.CreateSkeleton(Vector3.zero + Vector3.forward * 5);

            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                _entity.TakeDamageRequest.Invoke(50);

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _entity.MoveDirection.Value = input;
            _entity.RotationDirection.Value = input;
        }
    }
}
