using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdatdbleSystem
    {
        private readonly EntitiesLifeContext _lifeContext;

        private Entity _entity;

        private ReactiveVariable<bool> _isDead;

        private ReactiveVariable<bool> _inDeathProcess;

        public SelfReleaseSystem(EntitiesLifeContext lifeContext)
        {
            _lifeContext = lifeContext;
        }

        public void OnInit(Entity entity)
        {
             _entity = entity;
            _isDead = _entity.IsDead;
            _inDeathProcess = _entity.InDeathProcess;
        }

        public void OnUpdate(float deltaTimeS)
        {
            if (_isDead.Value && _inDeathProcess.Value == false)
                _lifeContext.Release(_entity);
        }
    }
}
