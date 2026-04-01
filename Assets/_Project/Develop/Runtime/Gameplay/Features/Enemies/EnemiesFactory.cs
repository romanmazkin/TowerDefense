using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class EnemiesFactory
    {
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;
        private ConfigsProviderService _configsProviderService;
        private EntitiesLifeContext _entitiesLifeContext;

        [Inject]
        public void Construct(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory,
            EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case SkeletonConfig skeletonConfig:
                    entity = _entitiesFactory.CreateSkeleton(position, skeletonConfig); 
                    _brainsFactory.CreateSkeletonBrain(entity);
                    break;

                default:
                    throw new ArgumentException($"Not support {config.GetType()} type config");
            }

            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));

            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
