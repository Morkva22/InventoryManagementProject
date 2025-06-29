using System;
using System.Collections.Generic;
using System.Windows;
using ManagementSystem;
using DesktopApplication.Views;

namespace DesktopApplication.Services
{
    public class WindowService : IWindowService
    {
        public ProductModel ShowProductEditDialog(ProductModel product)
        {
            var categories = new List<CategoryModel>(); 
            var suppliers = new List<SupplierModel>(); 
            var editWindow = new ProductEditWindow(product, categories, suppliers);
            
            if (editWindow.ShowDialog() == true)
            {
                return editWindow.Product;
            }
            
            return null;
        }

        public CategoryModel ShowCategoryEditDialog(CategoryModel category)
        {
            var editWindow = new CategoryEditWindow(category);
            
            if (editWindow.ShowDialog() == true)
            {
                return editWindow.Category;
            }
            
            return null;
        }

        public SupplierModel ShowSupplierEditDialog(SupplierModel supplier)
        {
            var editWindow = new SupplierEditWindow(supplier);
            
            if (editWindow.ShowDialog() == true)
            {
                return editWindow.Supplier;
            }
            
            return null;
        }

        public bool ShowConfirmation(string message, string title = "Confirmation")
        {
            var result = MessageBox.Show(message, title, 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string message, string title = "Information")
        {
            MessageBox.Show(message, title, 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}