using System.Windows.Controls;

namespace DesktopApplication.Services
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);
        void RegisterView<TView>(string viewName) where TView : UserControl;
        void SetContentControl(ContentControl contentControl);
    }
}