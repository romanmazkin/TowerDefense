using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Zenject;

namespace Assets._Project.Develop.Runtime.Utilities.Timer
{
    public class TimerServiceFactory
    {
        private ICoroutinesPerformer _coroutinesPerformer;

        [Inject]
        public void Construct(ICoroutinesPerformer coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
        }

        public TimerService Create(float cooldown)
            => new TimerService(cooldown, _coroutinesPerformer);
    }
}
