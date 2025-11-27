namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class TestPopupPresenter : PopupPresenterBase
    {
        private readonly TestPopupView _view;

        public TestPopupPresenter(TestPopupView popupView)
        {
            _view = popupView;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetText("Test title");
        }
    }
}
