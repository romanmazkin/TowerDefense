using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegisteryService _registeryService;

        private Entity _linkedEntity;

        public Entity LinkedEntity => _linkedEntity;

        public void Initialize(CollidersRegisteryService registeryService)
        {
            _registeryService = registeryService;
        }

        public void Link(Entity entity)
        {
            _linkedEntity = entity;

            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if(registrators != null )
                foreach(MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            foreach(Collider collder in GetComponentsInChildren<Collider>())
                _registeryService.Register(collder, entity);
        }

        public void Cleanup(Entity entity)
        {
            foreach (Collider collder in GetComponentsInChildren<Collider>())
                _registeryService.Unregister(collder);

            _linkedEntity = null;
        }
    }
}
