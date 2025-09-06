namespace Assets._Project.Develop.Runtime.Utilities.LoadingScreen
{
    public interface ILoadingScreen
    {
        bool IsShow { get; }
        void Show();
        void Hide();
    }
}
