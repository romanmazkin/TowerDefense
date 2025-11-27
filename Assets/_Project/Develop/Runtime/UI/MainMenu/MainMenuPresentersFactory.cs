using UnityEngine;
using Zenject;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private ProjectPresentersFactory _presentersFactory;
        private MainMenuPopupService _popupService;

        public MainMenuPresentersFactory(
            ProjectPresentersFactory presentersFactory
            , MainMenuPopupService popupService)
        {
            _presentersFactory = presentersFactory;
            _popupService = popupService;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            Debug.Log("Create view");
            return new MainMenuScreenPresenter(
                view,
                _presentersFactory,
                _popupService);


        }
    }
}
