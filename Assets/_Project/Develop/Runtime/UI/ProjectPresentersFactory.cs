using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Zenject;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private ConfigsProviderService _configProviderService;

        [Inject]
        private void Construct(ConfigsProviderService configsProviderService)
        {
            _configProviderService = configsProviderService;
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
    }
}
