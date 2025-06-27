using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Services;
using CommunityToolkit.Mvvm.Messaging;
using DesktopApplication.Messages;

namespace DesktopApplication.Views
{
    public partial class MainMenuControl : UserControl
    {
        private readonly INavigationService _navigationService;
        private Button _currentActiveButton;

        public MainMenuControl(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
            
            // Подписываемся на сообщения
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, (r, m) => 
            {
                if (m.ViewName == "Products") UpdateActiveButton(BtnProducts);
                else if (m.ViewName == "Categories") UpdateActiveButton(BtnCategories);
                else if (m.ViewName == "Suppliers") UpdateActiveButton(BtnSuppliers);
                else if (m.ViewName == "Home") UpdateActiveButton(BtnReports);
            });

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            _navigationService.NavigateTo("Products");
        }

        private void NavigateToView(string viewName, Button activeButton)
        {
            _navigationService.NavigateTo(viewName);
            UpdateActiveButton(activeButton);
            WeakReferenceMessenger.Default.Send(new NavigationMessage(viewName));
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e) 
            => NavigateToView("Products", BtnProducts);

        private void btnCategories_Click(object sender, RoutedEventArgs e) 
            => NavigateToView("Categories", BtnCategories);

        private void btnSuppliers_Click(object sender, RoutedEventArgs e) 
            => NavigateToView("Suppliers", BtnSuppliers);

        private void btnReports_Click(object sender, RoutedEventArgs e) 
            => NavigateToView("Home", BtnReports);

        private void UpdateActiveButton(Button activeButton)
        {
            if (_currentActiveButton != null)
                _currentActiveButton.Style = (Style)FindResource("PrimaryButton");
            
            activeButton.Style = (Style)FindResource("ActiveButtonStyle");
            _currentActiveButton = activeButton;
        }
    }
}