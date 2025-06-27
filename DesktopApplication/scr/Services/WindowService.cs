using System.Windows;
using ManagementSystem;
using DesktopApplication.Views; // Важно!

namespace DesktopApplication.Services
{
    public class WindowService : IWindowService
    {
        public bool ShowProductEditDialog(ProductModel product)
        {
            var dialog = new ProductEditWindow(product);
            return dialog.ShowDialog() == true;
        }

        public bool ShowCategoryEditDialog(CategoryModel category)
        {
            var dialog = new CategoryEditWindow(category);
            return dialog.ShowDialog() == true;
        }

        public bool ShowSupplierEditDialog(SupplierModel supplier)
        {
            var dialog = new SupplierEditWindow(supplier);
            return dialog.ShowDialog() == true;
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