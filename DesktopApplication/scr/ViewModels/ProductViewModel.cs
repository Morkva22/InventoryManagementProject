using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;
using DesktopApplication.Services;
using DesktopApplication.Views;

namespace DesktopApplication.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository;

        [ObservableProperty]
        private ProductModel? selectedProduct;

        [ObservableProperty]
        private string searchText = "";

        public ObservableCollection<ProductModel> Products { get; } = new();
        public ObservableCollection<ProductModel> FilteredProducts { get; } = new();

        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductViewModel(ISupabaseClientService repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            
            LoadProductsCommand = new AsyncRelayCommand(LoadProducts);
            AddProductCommand = new AsyncRelayCommand(AddProduct);
            EditProductCommand = new AsyncRelayCommand(EditProduct, () => SelectedProduct != null);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProduct, () => SelectedProduct != null);
            SearchCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(ApplyFilter);

            // Subscribe to search text changes
            PropertyChanged += (s, e) => 
            {
                if (e.PropertyName == nameof(SearchText))
                    ApplyFilter();
            };

            // Load products on initialization
            _ = LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                var products = await _repository.GetAllProducts();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddProduct()
        {
            try
            {
                var newProduct = new ProductModel
                {
                    Name = "New Product",
                    Description = "",
                    PurchasePrice = 1,
                    SellingPrice = 1,
                    CategoryId = 1,
                    SupplierId = 1,
                    CreatedAt = DateTime.Now
                };

                // Get lists of categories and suppliers
                var categories = await _repository.GetAllCategories();
                var suppliers = await _repository.GetAllSuppliers();
                
                var editWindow = new ProductEditWindow(newProduct, categories.ToList(), suppliers.ToList());

                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.Product.IsValid(out string errorMessage))
                    {
                        editWindow.Product.Id = 0; // Reset ID for new product
                        await _repository.AddProduct(editWindow.Product);
                        await LoadProducts();
                        MessageBox.Show("Product successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task EditProduct()
        {
            if (SelectedProduct == null) return;

            try
            {
                // Get lists of categories and suppliers
                var categories = await _repository.GetAllCategories();
                var suppliers = await _repository.GetAllSuppliers();
                
                var editWindow = new ProductEditWindow(SelectedProduct, categories.ToList(), suppliers.ToList());

                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.Product.IsValid(out string errorMessage))
                    {
                        editWindow.Product.Id = SelectedProduct.Id; // Preserve original ID
                        await _repository.UpdateProduct(editWindow.Product);
                        await LoadProducts();
                        MessageBox.Show("Product successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete product '{SelectedProduct.Name}'?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _repository.DeleteProduct(SelectedProduct.Id);
                    await LoadProducts();
                    SelectedProduct = null;
                    MessageBox.Show("Product successfully deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplyFilter()
        {
            FilteredProducts.Clear();
            
            var filtered = string.IsNullOrWhiteSpace(SearchText) 
                ? Products 
                : Products.Where(p => 
                    p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            foreach (var product in filtered)
            {
                FilteredProducts.Add(product);
            }
        }

        partial void OnSelectedProductChanged(ProductModel? value)
        {
            ((AsyncRelayCommand)EditProductCommand).NotifyCanExecuteChanged();
            ((AsyncRelayCommand)DeleteProductCommand).NotifyCanExecuteChanged();
        }
    }
}