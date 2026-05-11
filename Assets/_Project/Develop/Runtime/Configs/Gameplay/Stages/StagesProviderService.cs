using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using Zenject;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Stages
{
    public class StagesProviderService : IDisposable
    {
        public ReactiveVariable<int> _currentStageNumber;
        public ReactiveVariable<StageResults> _currentStageResult;

        private ConfigsProviderService _configsProviderService;
        private LevelConfig _levelConfig;
        private StagesFactory _stagesFactory;
        private SceneLoadingData _levelLoadingData;

        private IStage _currentStage;

        private IDisposable _stageEndedDisposable;

        [Inject]
        public void Construct(
            ConfigsProviderService configsProviderService,
            StagesFactory stagesFactory,
            SceneLoadingData levelLoadingData)
        {
            _configsProviderService = configsProviderService;
            _stagesFactory = stagesFactory;
            _levelLoadingData = levelLoadingData;
            _levelConfig = _configsProviderService.GetConfig<LevelsListConfig>().GetBy(_levelLoadingData.Level);
        }

        public IReadOnlyVariable<int> CurrentStageNumber => _currentStageNumber;

        public IReadOnlyVariable<StageResults> CurrentStageResult => _currentStageResult;

        public int StagesCount => _levelConfig.StageConfigs.Count;

        public bool HasNextStage() => CurrentStageNumber.Value < StagesCount;

        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException();

            if (_currentStage != null)
                CleanupCurrent();

            _currentStageNumber.Value++;
            _currentStageResult.Value = StageResults.Uncompleted;

            _currentStage = _stagesFactory.Create(_levelConfig.StageConfigs[_currentStageNumber.Value - 1]);
        }

        public void StartCurrent() 
        {
            _stageEndedDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
            _currentStage.Start();
        }

        private void OnStageCompleted()
        {
            _currentStageResult.Value = StageResults.Completed;
        }

        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);

        public void CleanupCurrent() => _currentStage.Cleanup();

        public void Dispose()
        {
            _currentStage?.Dispose();
            _stageEndedDisposable?.Dispose();
        }
    }
}
