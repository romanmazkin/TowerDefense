using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        [Inject]
        public void Construct()
        {
        }

        public Entity CreateTestEntity()
        {
            Entity entity = new Entity();

            entity.AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) });
            entity.AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) });

            return entity;
        }
    }
}
