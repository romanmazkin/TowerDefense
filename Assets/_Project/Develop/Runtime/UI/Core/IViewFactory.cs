using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public interface IViewFactory
    {
        TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView;
        void Release<TView>(TView view) where TView : MonoBehaviour, IView;
    }
}
