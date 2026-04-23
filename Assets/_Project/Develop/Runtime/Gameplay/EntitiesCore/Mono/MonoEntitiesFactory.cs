using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntitiesFactory : IInitializable, IDisposable
    {
        private ResourcesAssetsLoader _resources;

        private EntitiesLifeContext _lifeContext;

        private CollidersRegisteryService _collidersRegisteryService;

        private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

        [Inject]
        public void Construct(
            ResourcesAssetsLoader resourcesAssetsLoader,
            EntitiesLifeContext entitiesLifeContext,
            CollidersRegisteryService collidersRegisteryService)
        {
            _resources = resourcesAssetsLoader;
            _lifeContext = entitiesLifeContext;
            _collidersRegisteryService = collidersRegisteryService;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _resources.Load<MonoEntity>(path);

            MonoEntity viewInstance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            viewInstance.Initialize(_collidersRegisteryService);

            viewInstance.Link(entity);

            _entityToMono.Add(entity, viewInstance);

            return viewInstance;
        }

        public void Initialize()
        {
            _lifeContext.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _lifeContext.Released -= OnEntityReleased;

            foreach (Entity entity in _entityToMono.Keys)
                CleanupFor(entity);

            _entityToMono.Clear();
        }


        private void OnEntityReleased(Entity entity)
        {
            CleanupFor(entity);

            _entityToMono.Remove(entity);
        }

        private void CleanupFor(Entity entity)
        {
            MonoEntity monoEntity = _entityToMono[entity];
            monoEntity.Cleanup(entity);
            Object.Destroy(monoEntity.gameObject);
        }
    }
}
