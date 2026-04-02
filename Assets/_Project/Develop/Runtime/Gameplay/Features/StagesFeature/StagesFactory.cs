using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using System;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private EnemiesFactory _enemiesFactory;

        private EntitiesLifeContext _entitiesLifeContext;

        [Inject]
        public void Construct(
            EnemiesFactory enemiesFactory,
            EntitiesLifeContext entitiesLifeContext)
        {
            _enemiesFactory = enemiesFactory;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IStage Create(StageConfig stageConfig)
        {
            switch (stageConfig)
            {
                case ClearAllEnemiesStageConfig clearAllEnemiesStageConfig:
                    return new ClearAllEnemiesStage(
                        clearAllEnemiesStageConfig,
                        _enemiesFactory,
                        _entitiesLifeContext);

                default:
                    throw new ArgumentException($"Not supported {stageConfig.GetType()} type config");
            }
        }
    }
}
