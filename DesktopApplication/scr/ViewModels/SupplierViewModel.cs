using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ManagementSystem;
using DesktopApplication.Services;
using DesktopApplication.Views;

namespace DesktopApplication.ViewModels
{
    public partial class SupplierViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository;

        public ObservableCollection<SupplierModel> Suppliers { get; } = new();
        
        [ObservableProperty]
        private SupplierModel? selectedSupplier;

        [ObservableProperty]
        private string searchText = "";

        public ObservableCollection<SupplierModel> FilteredSuppliers { get; } = new();

        public ICommand LoadSuppliersCommand { get; }
        public ICommand AddSupplierCommand { get; }
        public ICommand EditSupplierCommand { get; }
        public ICommand DeleteSupplierCommand { get; }
        public ICommand SearchCommand { get; }

        public SupplierViewModel(ISupabaseClientService repository)
        {
            _repository = repository;
            
            LoadSuppliersCommand = new AsyncRelayCommand(LoadSuppliers);
            AddSupplierCommand = new AsyncRelayCommand(AddSupplier);
            EditSupplierCommand = new AsyncRelayCommand(EditSupplier, () => SelectedSupplier != null);
            DeleteSupplierCommand = new AsyncRelayCommand(DeleteSupplier, () => SelectedSupplier != null);
            SearchCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(ApplyFilter);

            // Subscribe to search text changes
            PropertyChanged += (s, e) => 
            {
                if (e.PropertyName == nameof(SearchText))
                    ApplyFilter();
            };
        }

        private async Task LoadSuppliers()
        {
            try
            {
                var suppliers = await _repository.GetAllSuppliers();
                Suppliers.Clear();
                foreach (var supplier in suppliers)
                {
                    Suppliers.Add(supplier);
                }
                ApplyFilter();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddSupplier()
        {
            var newSupplier = SupplierModel.CreateDefault();
            var editWindow = new SupplierEditWindow(newSupplier);
            
            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    if (editWindow.Supplier.IsValid(out string errorMessage))
                    {
                        await _repository.AddSupplier(editWindow.Supplier);
                        await LoadSuppliers();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error adding supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task EditSupplier()
        {
            if (SelectedSupplier == null) return;

            var editWindow = new SupplierEditWindow(SelectedSupplier);
            
            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    if (editWindow.Supplier.IsValid(out string errorMessage))
                    {
                        await _repository.UpdateSupplier(editWindow.Supplier);
                        await LoadSuppliers();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error updating supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task DeleteSupplier()
        {
            if (SelectedSupplier == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete supplier '{SelectedSupplier.Name}'?", 
                "Confirm Deletion", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _repository.DeleteSupplier(SelectedSupplier.Id);
                    await LoadSuppliers();
                    SelectedSupplier = null;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error deleting supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplyFilter()
        {
            FilteredSuppliers.Clear();
            
            var filtered = string.IsNullOrWhiteSpace(SearchText) 
                ? Suppliers 
                : Suppliers.Where(s => 
                    s.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(s.ContactPerson) && s.ContactPerson.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.Email) && s.Email.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)));

            foreach (var supplier in filtered)
            {
                FilteredSuppliers.Add(supplier);
            }
        }

        partial void OnSelectedSupplierChanged(SupplierModel? value)
        {
            ((AsyncRelayCommand)EditSupplierCommand).NotifyCanExecuteChanged();
            ((AsyncRelayCommand)DeleteSupplierCommand).NotifyCanExecuteChanged();
        }
    }
}