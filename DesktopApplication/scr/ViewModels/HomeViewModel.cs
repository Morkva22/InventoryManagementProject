using CommunityToolkit.Mvvm.ComponentModel;
using DesktopApplication.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopApplication.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository; 

        [ObservableProperty]
        private int _totalProducts;

        [ObservableProperty]
        private int _totalCategories;

        [ObservableProperty]
        private int _totalSuppliers;

        public HomeViewModel(ISupabaseClientService repository) 
        {
            _repository = repository;
            _ = LoadDashboardData(); 
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
                System.Diagnostics.Debug.WriteLine($"Error downloading data: {ex.Message}");
            }
        }
    }
}