using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using System;
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

        [SerializeField] private StageConfig _stageConfig;
        private StagesFactory _stagesFactory;
        private IStage _stage;
        
        private Entity _entity;
        private Entity _skeleton;
        private Entity _anotherSkeleton;

        [Inject]
        public void Construct(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory,
            MainHeroFactory mainHeroFactory,
            EnemiesFactory enemiesFactory,
            StagesFactory stagesFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
            _mainHeroFactory = mainHeroFactory;
            _enemiesFactory = enemiesFactory;
            _stagesFactory = stagesFactory;
        }

        public void Initialize()
        {
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);

            _stage = _stagesFactory.Create(_stageConfig);
            _stage.Completed.Subscribe(OnCompleted);
            _stage.Start();

            _isRunning = true;
        }

        private void OnCompleted()
        {
            Debug.Log("win");
            _stage.Cleanup();
        }

        private void Update()
        {
            if(_isRunning == false)
                return;

            _stage.Update(Time.deltaTime);
        }
    }
}
