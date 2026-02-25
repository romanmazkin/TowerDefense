using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private bool _isRunning;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        private Entity _entity;
        private Entity _skeleton;

        [Inject]
        public void Construct(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
        }

        public void Initialize()
        {
        }

        public void Run()
        {
            _entity = _entitiesFactory.CreateHero(Vector3.zero);
            _skeleton = _entitiesFactory.CreateSkeleton(Vector3.zero + Vector3.forward * 5);

            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                _entity.TakeDamageRequest.Invoke(50);

            if (Input.GetKeyDown(KeyCode.R))
                _entity.StartAttackRequest.Invoke();

            if (Input.GetKeyDown(KeyCode.I))
                _brainsFactory.CreateSkeletonBrain(_skeleton);

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _entity.MoveDirection.Value = input;
            _entity.RotationDirection.Value = input;
        }
    }
}
