using System.Windows;
using ManagementSystem;

namespace DesktopApplication.Views
{
    public partial class SupplierEditWindow : Window
    {
        public SupplierModel Supplier { get; }

        public SupplierEditWindow(SupplierModel supplier)
        {
            InitializeComponent();
            Supplier = supplier.Clone();
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