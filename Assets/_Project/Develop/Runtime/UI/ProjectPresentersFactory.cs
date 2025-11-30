using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Core.TestPopup;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Zenject;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private ConfigsProviderService _configProviderService;
        private WalletService _walletService;
        private ViewsFactory _viewsFactory;

        ICoroutinesPerformer _performer;

        [Inject]
        public void Construct(ICoroutinesPerformer coroutinesPerformer)
        {
            _performer = coroutinesPerformer; 
        }

        public ProjectPresentersFactory(
            ConfigsProviderService configProviderService,
            WalletService walletService,
            ViewsFactory viewsFactory)
        {
            _configProviderService = configProviderService;
            _walletService = walletService;
            _viewsFactory = viewsFactory;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            return new CurrencyPresenter(
                currency,
                currencyType,
                _configProviderService.GetConfig<CurrencyIconsConfig>(),
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(_walletService, this, _viewsFactory, view);
        }

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        {
            return new TestPopupPresenter(view, _performer);
        }
    }
}
