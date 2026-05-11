using System;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class GameplayStatesContext : IDisposable
    {
        private GameplayStateMachine _gameplayStateMachine;

        private bool _isRunning;

        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Dispose()
        {
            _isRunning = false;
            _gameplayStateMachine.Dispose();
        }

        public void Run()
        {
            _gameplayStateMachine.Enter();
            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            _gameplayStateMachine.Update(deltaTime);
        }
    }
}
