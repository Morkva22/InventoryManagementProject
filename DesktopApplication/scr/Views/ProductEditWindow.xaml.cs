using System.Windows;
using ManagementSystem;

namespace DesktopApplication.Views
{
    public partial class ProductEditWindow : Window
    {
        public ProductModel Product { get; }

        public ProductEditWindow(ProductModel product)
        {
            InitializeComponent();
            Product = product.Clone();
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
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