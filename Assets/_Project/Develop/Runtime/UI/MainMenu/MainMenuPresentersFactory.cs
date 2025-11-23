using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory : IInitializable
    {
        private ProjectPresentersFactory _presentersFactory;

        public MainMenuPresentersFactory(ProjectPresentersFactory presentersFactory)
        {
            _presentersFactory = presentersFactory;
        }

        //[Inject]
        //private void Construct(ProjectPresentersFactory projectPresentersFactory)
        //{
        //    _presentersFactory = projectPresentersFactory;
        //}

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            Debug.Log("Create view");
            return new MainMenuScreenPresenter(
                view,
                _presentersFactory);


        }

        public void Initialize()
        {
            Debug.Log("YAY");
        }
    }
}
