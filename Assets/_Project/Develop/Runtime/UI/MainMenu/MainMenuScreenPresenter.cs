using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System;
using System.Collections.Generic;
using Zenject;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly MainMenuPopupService _popupService;

        private List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView mainMenuScreenView,
            ProjectPresentersFactory presentersFactory,
            MainMenuPopupService popupService)
        {
            _screen = mainMenuScreenView;
            _presentersFactory = presentersFactory;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _screen.OpenTestPopupButtonClicked += OnOpenTestPopupButtonClicked;

            CreateWallet();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.OpenTestPopupButtonClicked -= OnOpenTestPopupButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _presentersFactory.CreateWalletPresenter(_screen.WalletView);

            _childPresenters.Add(walletPresenter);
        }

        private void OnOpenTestPopupButtonClicked()
        {
            _popupService.OpenTestPopup();
        }
    }
}
