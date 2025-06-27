using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Services;
using DesktopApplication.Views;

namespace DesktopApplication
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _navigationService = serviceProvider.GetRequiredService<INavigationService>();
            
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var mainMenu = _serviceProvider.GetRequiredService<MainMenuControl>();
            MainContainer.Children.Add(mainMenu);
            
            // Находим ContentControl в MainMenu
            if (mainMenu.FindName("MainContent") is ContentControl contentControl)
            {
                _navigationService.SetContentControl(contentControl);
            }
            else
            {
                MessageBox.Show("Ошибка инициализации интерфейса", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            Loaded -= OnWindowLoaded;
            base.OnClosed(e);
        }
    }
}