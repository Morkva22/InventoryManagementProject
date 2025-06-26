using System.Windows.Controls;

namespace DesktopApplication.Services
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);
        void RegisterView(string viewName, UserControl view);
    }
}