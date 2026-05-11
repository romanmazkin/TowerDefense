using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class StagesProcessState : State, IUpdatableState
    {
        private readonly StagesProviderService _stagesProviderService;

        public StagesProcessState(StagesProviderService stagesProviderService)
        {
            _stagesProviderService = stagesProviderService;
        }

        public override void Enter()
        {
            base.Enter();

            _stagesProviderService.SwitchToNext();
            _stagesProviderService.StartCurrent();
        }

        public void Update(float deltaTime)
        {
            _stagesProviderService.UpdateCurrent(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _stagesProviderService.CleanupCurrent();
        }
    }
}
