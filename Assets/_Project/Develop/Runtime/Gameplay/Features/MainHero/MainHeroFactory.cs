using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;
        private ConfigsProviderService _configsProviderService;
        private EntitiesLifeContext _entitiesLifeContext;

        [Inject]
        public void Construct(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory,
            ConfigsProviderService configsProviderService,
            EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
            _configsProviderService = configsProviderService;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public Entity Create(Vector3 position)
        {
            HeroConfig config = _configsProviderService.GetConfig<HeroConfig>();

            Entity entity = _entitiesFactory.CreateHero(position, config);

            entity
                .AddIsMainHero()
                .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero));

            entity.AddCurrentTarget();
            _brainsFactory.CreateMainHeroBrain(entity, new NearestDamageableTargetSelector(entity));

            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
