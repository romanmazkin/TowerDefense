using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeathSystem : IInitializableSystem, IUpdatdbleSystem
    {
        private ReactiveVariable<bool> _isDead;

        private ReactiveVariable<float> _currentHealth;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            _currentHealth = entity.CurrentHealth;
        }

        public void OnUpdate(float deltaTimeS)
        {
            if(_isDead.Value == true)
                return;

            if(_currentHealth.Value<= 0)
            {
                _isDead.Value = true;
                Debug.Log("Я умер");
            }
        }
    }
}
