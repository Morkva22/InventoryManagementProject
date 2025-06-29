using System.Windows;
using ManagementSystem;
using System.Text.RegularExpressions;

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
            // Validation using model method
            if (!Supplier.IsValid(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Additional email validation
            if (!string.IsNullOrEmpty(Supplier.Email) && !IsValidEmail(Supplier.Email))
            {
                MessageBox.Show("Please enter a valid email address", "Error",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Additional phone validation
            if (!string.IsNullOrEmpty(Supplier.Phone) && !IsValidPhone(Supplier.Phone))
            {
                MessageBox.Show("Please enter a valid phone number", "Error",
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

        private bool IsValidEmail(string email)
        {
            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            try
            {
                // Simple validation for digits and allowed characters
                var phoneRegex = new Regex(@"^[\d\s\-\+\(\)]+$");
                return phoneRegex.IsMatch(phone) && phone.Length >= 10;
            }
            catch
            {
                return false;
            }
        }
    }
}