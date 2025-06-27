using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopApplication.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _viewRegistry = new();
        private ContentControl _contentControl;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetContentControl(ContentControl contentControl)
        {
            _contentControl = contentControl ?? throw new ArgumentNullException(nameof(contentControl));
        }

        public void RegisterView<TView>(string viewName) where TView : UserControl
        {
            _viewRegistry[viewName] = typeof(TView);
        }

        public void NavigateTo(string viewName)
        {
            if (_contentControl == null)
                throw new InvalidOperationException("ContentControl is not set. Call SetContentControl first.");

            if (_viewRegistry.TryGetValue(viewName, out var viewType))
            {
                var view = (UserControl)_serviceProvider.GetService(viewType);
                _contentControl.Content = view;
            }
            else
            {
                throw new KeyNotFoundException($"View '{viewName}' is not registered");
            }
        }
    }
}