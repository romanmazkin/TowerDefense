using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using Zenject;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroHolderService : IInitializable, IDisposable
    {
        private EntitiesLifeContext _lifeContext;

        private ReactiveEvent<Entity> _heroRegistred = new();
        private Entity _mainHero;

        [Inject] public void Construct(EntitiesLifeContext lifeContext)
        {
            _lifeContext = lifeContext;
        }

        public IReadOnlyEvent<Entity> HeroRegistred => _heroRegistred;

        public Entity MainHero => _mainHero;

        public void Initialize()
        {
            _lifeContext.Added += OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsMainHero>())
            {
                _lifeContext.Added -= OnEntityAdded;
                _mainHero = entity;
                _heroRegistred?.Invoke(_mainHero);
            }
        }

        public void Dispose()
        {
            _lifeContext.Added -= OnEntityAdded;
        }
    }
}
