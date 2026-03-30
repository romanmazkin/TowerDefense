using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
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

        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private SkeletonConfig _skeletonConfig;
        
        private Entity _entity;
        private Entity _skeleton;
        private Entity _anotherSkeleton;

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
            _entity = _entitiesFactory.CreateHero(Vector3.zero, _heroConfig);
            _entity.AddCurrentTarget();
            _brainsFactory.CreateMainHeroBrain(_entity, new NearestDamageableTargetSelector(_entity));

            _skeleton = _entitiesFactory.CreateSkeleton(Vector3.zero + Vector3.forward * 5, _skeletonConfig);
            _anotherSkeleton = _entitiesFactory.CreateSkeleton(Vector3.zero - Vector3.forward * 5, _skeletonConfig);

            _brainsFactory.CreateSkeletonBrain(_skeleton);
            _brainsFactory.CreateSkeletonBrain(_anotherSkeleton);

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

            //if (Input.GetKeyDown(KeyCode.I))
            //    _brainsFactory.CreateSkeletonBrain(_skeleton);
        }
    }
}
