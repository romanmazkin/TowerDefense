using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private EntitiesLifeContext _entitiesLifeContext;
        private MonoEntitiesFactory _monoEntitiesFactory;

        [Inject]
        public void Construct(
            EntitiesLifeContext entitiesLifeContext,
            MonoEntitiesFactory monoEntitiesFactory)
        {
            _entitiesLifeContext = entitiesLifeContext;
            _monoEntitiesFactory = monoEntitiesFactory;
        }

        public Entity CreateTestEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/TestEntity");

            entity
                .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
                .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) });

            entity.AddSystem(new RigidbodyMovementSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
