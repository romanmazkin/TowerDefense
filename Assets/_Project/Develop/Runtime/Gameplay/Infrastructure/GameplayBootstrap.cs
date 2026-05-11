using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.States;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private WalletService _walletService;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;
        private GameplayStatesContext _gameplayStatesContext;
        private MainHeroFactory _mainHeroFactory;

        [Inject]
        public void Construct(
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            WalletService walletService,
            EntitiesLifeContext entitiesLifeContext,
            AIBrainsContext aIBrainsContext,
            GameplayStatesContext gameplayStatesContext,
            MainHeroFactory mainHeroFactory)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
            _entitiesLifeContext = entitiesLifeContext;
            _brainsContext = aIBrainsContext;
            _gameplayStatesContext = gameplayStatesContext;
            _mainHeroFactory = mainHeroFactory;
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Init gameplay");

            Debug.Log($"Loaded level");

            _mainHeroFactory.Create(Vector3.zero);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start gameplay scene");

            _gameplayStatesContext.Run();
        }

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _gameplayStatesContext?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.F))
            {
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}