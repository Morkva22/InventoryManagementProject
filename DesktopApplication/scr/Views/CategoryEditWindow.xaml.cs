using System.Windows;
using ManagementSystem;

namespace DesktopApplication.Views
{
    public partial class CategoryEditWindow : Window
    {
        public CategoryModel Category { get; }

        public CategoryEditWindow(CategoryModel category)
        {
            InitializeComponent();
            Category = category.Clone();
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!Category.IsValid(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error",
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