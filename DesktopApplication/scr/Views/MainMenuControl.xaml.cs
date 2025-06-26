using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Services;
using DesktopApplication.Views;
using ManagementSystem;

namespace DesktopApplication.Views
{
    public partial class MainMenuControl : UserControl
    {
        private readonly INavigationService _navigationService;

        public MainMenuControl(ISupabaseRepository repository)
        {
            InitializeComponent();

            // Создаем навигационный сервис
            _navigationService = new NavigationService(MainContent);

            // Регистрируем представления
            RegisterViews(repository);

            // Навигация к представлению продуктов по умолчанию
            _navigationService.NavigateTo("Products");
        }

        private void RegisterViews(ISupabaseRepository repository)
        {
            // Создание и регистрация представлений
            var productView = new ProductsUserControl(new ViewModels.ProductViewModel(repository));
            _navigationService.RegisterView("Products", productView);

            // Заглушки для других представлений
            _navigationService.RegisterView("Categories", new PlaceholderUserControl("Категории"));
            _navigationService.RegisterView("Suppliers", new PlaceholderUserControl("Поставщики"));
            _navigationService.RegisterView("Reports", new PlaceholderUserControl("Отчеты"));
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Products");
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Categories");
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Suppliers");
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Reports");
        }
    }
}