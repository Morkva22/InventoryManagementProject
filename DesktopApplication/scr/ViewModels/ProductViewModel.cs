using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;

namespace DesktopApplication.ViewModels
{
    public class ProductViewModel
    {
        private readonly ISupabaseRepository _repository;

        public ObservableCollection<ProductModel> Products { get; private set; } = new ObservableCollection<ProductModel>();
        public ProductModel SelectedProduct { get; set; }

        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public ProductViewModel(ISupabaseRepository repository)
        {
            _repository = repository;

            LoadProductsCommand = new AsyncRelayCommand(LoadProducts);
            AddProductCommand = new AsyncRelayCommand(AddProduct);
            UpdateProductCommand = new AsyncRelayCommand(UpdateProduct);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProduct);
        }

        private async Task LoadProducts()
        {
            var products = await _repository.GetAllProducts();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async Task AddProduct()
        {
            // This would open a dialog or navigate to an add product page
            // For now, just a placeholder
            var newProduct = new ProductModel
            {
                Name = "New Product",
                Description = "Description",
                CategoryID = 1, // Default category ID, should be replaced with actual value
                SupplierID = 1, // Default supplier ID, should be replaced with actual value
                PurchasePrice = 0.00m,
                SellingPrice = 0.00m
            };

            await _repository.AddProduct(newProduct);
            await LoadProducts();
        }

        private async Task UpdateProduct()
        {
            if (SelectedProduct != null)
            {
                await _repository.UpdateProduct(SelectedProduct);
                await LoadProducts();
            }
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                await _repository.DeleteProduct(SelectedProduct.id);
                await LoadProducts();
            }
        }
    }
}