using CommunityToolkit.Mvvm.ComponentModel;
using DesktopApplication.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopApplication.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository; // Используйте интерфейс

        [ObservableProperty]
        private int _totalProducts;

        [ObservableProperty]
        private int _totalCategories;

        [ObservableProperty]
        private int _totalSuppliers;

        public HomeViewModel(ISupabaseClientService repository) // Измените на интерфейс
        {
            _repository = repository;
            _ = LoadDashboardData(); // Async void избегаем
        }

        private async Task LoadDashboardData()
        {
            try
            {
                var products = await _repository.GetAllProducts();
                TotalProducts = products.Count();

                var categories = await _repository.GetAllCategories();
                TotalCategories = categories.Count();

                var suppliers = await _repository.GetAllSuppliers();
                TotalSuppliers = suppliers.Count();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}