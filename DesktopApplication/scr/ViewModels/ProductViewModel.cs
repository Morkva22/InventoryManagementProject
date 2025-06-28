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
                PurchasePrice = 1, // Минимальная цена вместо 0
                SellingPrice = 1,  // Минимальная цена вместо 0
                CategoryId = 1,    // Добавьте значение по умолчанию для CategoryId
                SupplierId = 1,    // Добавьте значение по умолчанию для SupplierId
                CreatedAt = DateTime.Now
            };

            // Используем WindowService для получения отредактированного продукта
            var editedProduct = _windowService.ShowProductEditDialog(newProduct);
            
            if (editedProduct != null)
            {
                try
                {
                    // Дополнительная валидация перед сохранением
                    if (editedProduct.PurchasePrice <= 0 || editedProduct.SellingPrice <= 0)
                    {
                        _windowService.ShowError("Цены должны быть больше 0");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(editedProduct.Name))
                    {
                        _windowService.ShowError("Название продукта не может быть пустым");
                        return;
                    }

                    await _repository.AddProduct(editedProduct);
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

            // Используем WindowService для получения отредактированного продукта
            var editedProduct = _windowService.ShowProductEditDialog(SelectedProduct);
            
            if (editedProduct != null)
            {
                try
                {
                    // Устанавливаем ID оригинального продукта для обновления
                    editedProduct.Id = SelectedProduct.Id;
                    
                    // Дополнительная валидация
                    if (editedProduct.PurchasePrice <= 0 || editedProduct.SellingPrice <= 0)
                    {
                        _windowService.ShowError("Цены должны быть больше 0");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(editedProduct.Name))
                    {
                        _windowService.ShowError("Название продукта не может быть пустым");
                        return;
                    }

                    await _repository.UpdateProduct(editedProduct);
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