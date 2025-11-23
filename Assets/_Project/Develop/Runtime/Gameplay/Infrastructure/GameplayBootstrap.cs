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
        SceneSwitcherService _sceneSwitcherService;
        ICoroutinesPerformer _coroutinesPerformer;
        SceneLoadingData _sceneLoadingData;
        WalletService _walletService;

        [Inject]
        public void Construct(
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            SceneLoadingData sceneLoadingData,
            WalletService walletService)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneLoadingData = sceneLoadingData;
            _walletService = walletService;
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Init gameplay");

            Debug.Log($"Loaded level {_sceneLoadingData.Level}");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start gameplay scene");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Gold " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log("Gold " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }
        }
    }
}
