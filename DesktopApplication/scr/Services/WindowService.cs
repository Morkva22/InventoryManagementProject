using System.Windows;
using ManagementSystem;
using DesktopApplication.Views;

namespace DesktopApplication.Services
{
    public class WindowService : IWindowService
    {
        public ProductModel ShowProductEditDialog(ProductModel product)
        {
            var dialog = new ProductEditWindow(product);
            if (dialog.ShowDialog() == true)
            {
                // Возвращаем отредактированный продукт
                return dialog.Product;
            }
            return null; // Пользователь отменил редактирование
        }

        public CategoryModel ShowCategoryEditDialog(CategoryModel category)
        {
            var dialog = new CategoryEditWindow(category);
            if (dialog.ShowDialog() == true)
            {
                return dialog.Category; // Предполагаем, что у CategoryEditWindow есть свойство Category
            }
            return null;
        }

        public SupplierModel ShowSupplierEditDialog(SupplierModel supplier)
        {
            var dialog = new SupplierEditWindow(supplier);
            if (dialog.ShowDialog() == true)
            {
                return dialog.Supplier; // Предполагаем, что у SupplierEditWindow есть свойство Supplier
            }
            return null;
        }

        public bool ShowConfirmation(string message, string title = "Подтверждение")
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) 
                   == MessageBoxResult.Yes;
        }

        public void ShowError(string message, string title = "Ошибка")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string message, string title = "Информация")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}