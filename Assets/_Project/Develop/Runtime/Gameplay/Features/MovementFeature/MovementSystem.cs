using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MovementSystem : IInitializableSystem, IUpdatdbleSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.GetComponent<MoveDirection>().Value;
            _moveSpeed = entity.GetComponent<MoveSpeed>().Value;
        }

        public void OnUpdate(float deltaTimeS)
        {
            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;

            Debug.Log("Speed is " + velocity.ToString());
        }
    }
}
