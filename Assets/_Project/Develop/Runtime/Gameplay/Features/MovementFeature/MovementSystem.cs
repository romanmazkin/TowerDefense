using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MovementSystem : IInitializableSystem, IUpdatdbleSystem
    {
        private Entity _entity;

        public void OnInit(Entity entity)
        {
            _entity = entity;
        }

        public void OnUpdate(float deltaTimeS)
        {
           ReactiveVariable<Vector3> moveDirection = _entity.GetComponent<MoveDirection>().Value;
           ReactiveVariable<float> moveSpeed = _entity.GetComponent<MoveSpeed>().Value;

            Vector3 velocity = moveDirection.Value.normalized * moveSpeed.Value;

            Debug.Log("Speed is " + velocity.ToString());
        }
    }
}
