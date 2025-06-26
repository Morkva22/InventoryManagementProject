using System.Collections.Generic;
using System.Windows.Controls;

namespace DesktopApplication.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _contentControl;
        private readonly Dictionary<string, UserControl> _views = new Dictionary<string, UserControl>();

        public NavigationService(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        public void NavigateTo(string viewName)
        {
            if (_views.ContainsKey(viewName))
            {
                _contentControl.Content = _views[viewName];
            }
        }

        public void RegisterView(string viewName, UserControl view)
        {
            if (!_views.ContainsKey(viewName))
            {
                _views.Add(viewName, view);
            }
            else
            {
                _views[viewName] = view;
            }
        }
    }
}