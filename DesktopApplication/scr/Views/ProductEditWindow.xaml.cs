using System.Windows;
using ManagementSystem;
using System.Globalization;

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
            // Валидация с использованием метода модели
            if (!Product.IsValid(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка валидации", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Дополнительная проверка на случай, если пользователь ввел некорректные данные
            if (Product.PurchasePrice <= 0)
            {
                MessageBox.Show("Цена закупки должна быть больше 0", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Product.SellingPrice <= 0)
            {
                MessageBox.Show("Цена продажи должна быть больше 0", "Ошибка", 
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