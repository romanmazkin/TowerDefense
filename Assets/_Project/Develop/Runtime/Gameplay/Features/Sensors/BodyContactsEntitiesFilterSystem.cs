using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class BodyContactsEntitiesFilterSystem : IInitializableSystem, IUpdatdbleSystem
    {
        private Buffer<Collider> _contacts;
        private Buffer<Entity> _contactsEntities;

        private readonly CollidersRegisteryService _registeryService;

        public BodyContactsEntitiesFilterSystem(CollidersRegisteryService registeryService)
        {
            _registeryService = registeryService;
        }

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactCollidersBuffer;
            _contactsEntities = entity.ContactEntitiesBuffer;
        }

        public void OnUpdate(float deltaTimeS)
        {
            _contactsEntities.Count = 0;

            for(int i = 0; i < _contacts.Count; i++)
            {
                Collider collider = _contacts.Items[i];

                Entity contactEntity = _registeryService.GetBy(collider);

                if(contactEntity != null)
                {
                    _contactsEntities.Items[_contactsEntities.Count] = contactEntity;
                    _contactsEntities.Count++;
                }
            }

            Debug.Log("Contscts: " + _contactsEntities.Count);
        }
    }
}
