using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public partial class Entity
    {
        public RigidBodyComponent RigidbodyC => GetComponent<RigidBodyComponent>();

        public Rigidbody Rigidbody => RigidbodyC.Value;

        public Entity AddRigidbody(Rigidbody value)
        {
            return AddComponent(new RigidBodyComponent() { Value = value });
        }

        public MoveDirection MoveDirectionC => GetComponent<MoveDirection>();

        public ReactiveVariable<Vector3> MoveDirection => MoveDirectionC.Value;

        public Entity AddMoveDirection(ReactiveVariable<Vector3> value)
        {
            return AddComponent(new MoveDirection() { Value = value });
        }

        public Entity AddMoveDirection()
        {
            return AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>() });
        }
    }
}
