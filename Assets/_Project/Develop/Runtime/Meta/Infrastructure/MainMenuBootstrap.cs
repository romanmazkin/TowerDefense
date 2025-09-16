using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        SceneSwitcherService _sceneSwitcherService;
        ICoroutinesPerformer _coroutinesPerformer;
        SceneLoadingData _sceneLoadingData;

        [Inject]
        public void Construct(
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            SceneLoadingData sceneLoadingData)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneLoadingData = sceneLoadingData;
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Init menu");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start main menu scene");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new SceneLoadingData(2)));
            }
        }
    }
}