using System.Windows;
using ManagementSystem;
using System.Collections.Generic;
using System;

namespace DesktopApplication.Views
{
    public partial class ProductEditWindow : Window
    {
        public ProductModel Product { get; }
        public List<CategoryModel> Categories { get; }
        public List<SupplierModel> Suppliers { get; }

        public ProductEditWindow(ProductModel product, List<CategoryModel> categories, List<SupplierModel> suppliers)
        {
            InitializeComponent();
            Product = product.Clone();
            Categories = categories ?? new List<CategoryModel>();
            Suppliers = suppliers ?? new List<SupplierModel>();
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validation using model method
            if (!Product.IsValid(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Additional validation in case user entered invalid data
            if (Product.PurchasePrice <= 0)
            {
                MessageBox.Show("Purchase price must be greater than 0", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Product.SellingPrice <= 0)
            {
                MessageBox.Show("Selling price must be greater than 0", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}