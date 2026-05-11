using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private PreparationTriggerService _preparationTriggerService;
        private StagesProviderService _stagesProviderService;
        private IInputService _inputService;
        private LevelsProgressionService _levelsProgressionService;
        private SceneLoadingData _sceneLoadingData;
        private PlayerDataProvider _playerDataProvider;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private MainHeroHolderService _mainHeroHolderService;

        [Inject]
        public void Construct(
            PreparationTriggerService preparationTriggerService,
            StagesProviderService stagesProviderService,
            IInputService inputService,
            LevelsProgressionService levelsProgressionService,
            SceneLoadingData sceneLoadingData,
            PlayerDataProvider playerDataProvider,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            MainHeroHolderService mainHeroHolderService)
        {
            _preparationTriggerService = preparationTriggerService;
            _stagesProviderService = stagesProviderService;
            _inputService = inputService;
            _levelsProgressionService = levelsProgressionService;
            _sceneLoadingData = sceneLoadingData;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public PreparationState CreatePreparationState()
        {
            return new PreparationState(_preparationTriggerService);
        }

        public StagesProcessState CreateStageProcessState()
        {
            return new StagesProcessState(_stagesProviderService);
        }

        public WinState CreateWinState()
        {
            return new WinState(
                _inputService,
                _sceneLoadingData,
                _levelsProgressionService,
                _playerDataProvider,
                _sceneSwitcherService,
                _coroutinesPerformer);
        }

        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _inputService,
                _sceneSwitcherService,
                _coroutinesPerformer);
        }

        public GameplayStateMachine CreateGameplayStateMachine(SceneLoadingData _sceneLoadingData)
        {
            PreparationTriggerService preparationTriggerService = _preparationTriggerService;
            StagesProviderService stagesProviderService = _stagesProviderService;
            MainHeroHolderService mainHeroHolderService = _mainHeroHolderService;

            GameplayStateMachine coreLoopState = CreateCoreLoopState();

            WinState winState = CreateWinState();
            DefeatState defeatState = CreateDefeatState();

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stagesProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stagesProviderService.HasNextStage() == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if(mainHeroHolderService.MainHero != null)
                        return mainHeroHolderService.MainHero.IsDead.Value;

                    return false;
                }));

            GameplayStateMachine gameplayCycle = new GameplayStateMachine();

            gameplayCycle.AddState(coreLoopState);
            gameplayCycle.AddState(winState);
            gameplayCycle.AddState(defeatState);

            gameplayCycle.AddTransition(coreLoopState, winState, coreLoopToWinStateCondition);
            gameplayCycle.AddTransition(coreLoopState, defeatState, coreLoopToDefeatStateCondition);

            return gameplayCycle;
        }

        public GameplayStateMachine CreateCoreLoopState()
        {
            PreparationTriggerService preparationTriggerService = _preparationTriggerService;
            StagesProviderService stagesProviderService = _stagesProviderService;

            PreparationState preparationState = CreatePreparationState();
            StagesProcessState stageProcessState = CreateStageProcessState();

            ICompositeCondition preparationToStageProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stagesProviderService.HasNextStage()));


            FuncCondition stageProcessToPreparationCondition =
                new FuncCondition(() => stagesProviderService.CurrentStageResult.Value == StageResults.Completed);

            GameplayStateMachine coreLoopState = new GameplayStateMachine();

            coreLoopState.AddState(preparationState);
            coreLoopState.AddState(stageProcessState);

            coreLoopState.AddTransition(preparationState, stageProcessState, preparationToStageProcessCondition);
            coreLoopState.AddTransition(stageProcessState, stageProcessState, preparationToStageProcessCondition);

            return coreLoopState;
        }
    }
}
