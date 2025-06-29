using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using DesktopApplication.Services;

namespace DesktopApplication.Views
{
    public partial class MainMenuControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        public MainMenuControl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _navigationService = serviceProvider.GetRequiredService<INavigationService>();
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Products");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Categories");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Suppliers");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Home");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}