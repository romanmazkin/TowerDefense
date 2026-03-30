using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
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
        private MainHeroFactory _mainHeroFactory;
        private EnemiesFactory _enemiesFactory;

        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private SkeletonConfig _skeletonConfig;
        
        private Entity _entity;
        private Entity _skeleton;
        private Entity _anotherSkeleton;

        [Inject]
        public void Construct(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory,
            MainHeroFactory mainHeroFactory,
            EnemiesFactory enemiesFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
            _mainHeroFactory = mainHeroFactory;
            _enemiesFactory = enemiesFactory;
        }

        public void Initialize()
        {
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);
            _skeleton = _enemiesFactory.Create(Vector3.zero + Vector3.forward * 5, _skeletonConfig);
            _anotherSkeleton = _enemiesFactory.Create(Vector3.zero - Vector3.forward * 5, _skeletonConfig);

            _isRunning = true;
        }

        private void Update()
        {
            if(_isRunning == false)
                return;
        }
    }
}
