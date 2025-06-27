using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private ProductModel _selectedProduct;

        public ObservableCollection<ProductModel> Products { get; } = new();

        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public ProductViewModel(ISupabaseClientService repository, IWindowService windowService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            
            LoadProductsCommand = new AsyncRelayCommand(LoadProducts);
            AddProductCommand = new AsyncRelayCommand(AddProduct);
            EditProductCommand = new AsyncRelayCommand(EditProduct);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProduct);
            
            LoadProducts();
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
            }
            catch (Exception ex)
            {
                _windowService.ShowError($"Ошибка загрузки продуктов: {ex.Message}");
            }
        }

        private async Task AddProduct()
        {
            var newProduct = new ProductModel
            {
                Name = "Новый продукт",
                Description = "",
                PurchasePrice = 0,
                SellingPrice = 0,
                CreatedAt = DateTime.Now
            };

            if (_windowService.ShowProductEditDialog(newProduct))
            {
                try
                {
                    await _repository.AddProduct(newProduct);
                    await LoadProducts();
                    _windowService.ShowInfo("Продукт успешно добавлен!");
                }
                catch (Exception ex)
                {
                    _windowService.ShowError($"Ошибка добавления: {ex.Message}");
                }
            }
        }

        private async Task EditProduct()
        {
            if (SelectedProduct == null)
            {
                _windowService.ShowError("Выберите продукт для редактирования");
                return;
            }

            var clone = SelectedProduct.Clone();
            if (_windowService.ShowProductEditDialog(clone))
            {
                try
                {
                    await _repository.UpdateProduct(clone);
                    await LoadProducts();
                    _windowService.ShowInfo("Продукт успешно обновлен!");
                }
                catch (Exception ex)
                {
                    _windowService.ShowError($"Ошибка обновления: {ex.Message}");
                }
            }
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct == null)
            {
                _windowService.ShowError("Выберите продукт для удаления");
                return;
            }

            if (_windowService.ShowConfirmation("Удалить выбранный продукт?"))
            {
                try
                {
                    await _repository.DeleteProduct(SelectedProduct.Id);
                    await LoadProducts();
                    _windowService.ShowInfo("Продукт удален!");
                }
                catch (Exception ex)
                {
                    _windowService.ShowError($"Ошибка удаления: {ex.Message}");
                }
            }
        }
    }
}