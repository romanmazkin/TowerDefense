using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private LevelsProgressionService _levelsProgressionService;
        private SceneLoadingData _sceneLoadingData;
        private PlayerDataProvider _playerDataProvider;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;

        public WinState(
            IInputService inputService,
            SceneLoadingData sceneLoadingData,
            LevelsProgressionService levelsProgressionService, 
            PlayerDataProvider playerDataProvider, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer) : base(inputService)
        {
            _levelsProgressionService = levelsProgressionService;
            _sceneLoadingData = sceneLoadingData;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Win");

            _levelsProgressionService.AddLevelToCompeted(_sceneLoadingData.Level);

            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
