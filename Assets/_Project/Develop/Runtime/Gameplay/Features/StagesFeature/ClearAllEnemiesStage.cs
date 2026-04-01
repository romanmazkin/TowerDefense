using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class ClearAllEnemiesStage : IStage
    {
        private ClearAllEnemiesStageConfig _config;

        private ReactiveEvent _completed = new();

        private EnemiesFactory _enemiesFactory;

        private bool _inProcess;

        public ClearAllEnemiesStage(
            ClearAllEnemiesStageConfig config, 
            EnemiesFactory enemiesFactory)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
        }

        public IReadOnlyEvent Completed => throw new System.NotImplementedException();

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
