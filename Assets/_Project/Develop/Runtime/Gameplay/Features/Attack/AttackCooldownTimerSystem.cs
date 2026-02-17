using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inAttackCooldown;

        private ReactiveEvent _endAttackEvent;

        private IDisposable _endAttackEventDisposable;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackCooldownCurrentTime;
            _initialTime = entity.AttackCooldownInitialTime;
            _inAttackCooldown = entity.InAttackCooldown;
            _endAttackEvent = entity.EndAttackEvent;

            _endAttackEventDisposable = _endAttackEvent.Subscribe(OnEndAttack);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackCooldown.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
            {
                _inAttackCooldown.Value = false;
                Debug.Log("End cooldown");
            }
        }

        private void OnEndAttack()
        {
            Debug.Log("Start cooldown");
            _currentTime.Value = _initialTime.Value;
            _inAttackCooldown.Value = true;
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;

        public void OnDispose()
        {
            _endAttackEventDisposable.Dispose();
        }
    }
}
