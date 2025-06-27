// SupplierViewModel.cs - исправленный
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class SupplierViewModel
    {
        private readonly ISupabaseClientService _repository;

        public ObservableCollection<SupplierModel> Suppliers { get; } = new();
        public SupplierModel SelectedSupplier { get; set; }

        public ICommand LoadSuppliersCommand { get; }
        public ICommand AddSupplierCommand { get; }
        public ICommand UpdateSupplierCommand { get; }
        public ICommand DeleteSupplierCommand { get; }

        public SupplierViewModel(ISupabaseClientService repository)
        {
            _repository = repository;
            
            LoadSuppliersCommand = new AsyncRelayCommand(LoadSuppliers);
            AddSupplierCommand = new AsyncRelayCommand(AddSupplier);
            UpdateSupplierCommand = new AsyncRelayCommand(UpdateSupplier);
            DeleteSupplierCommand = new AsyncRelayCommand(DeleteSupplier);
        }

        private async Task LoadSuppliers()
        {
            var suppliers = await _repository.GetAllSuppliers();
            Suppliers.Clear();
            foreach (var supplier in suppliers)
            {
                Suppliers.Add(supplier);
            }
        }

        private async Task AddSupplier()
        {
            var newSupplier = new SupplierModel 
            { 
                Name = "Новый поставщик",
                ContactPerson = "Контактное лицо",
                Phone = "",
                Email = ""
            };
            await _repository.AddSupplier(newSupplier);
            await LoadSuppliers();
        }

        private async Task UpdateSupplier()
        {
            if (SelectedSupplier != null)
            {
                await _repository.UpdateSupplier(SelectedSupplier);
                await LoadSuppliers();
            }
        }

        private async Task DeleteSupplier()
        {
            if (SelectedSupplier != null)
            {
                await _repository.DeleteSupplier(SelectedSupplier.Id);
                await LoadSuppliers();
            }
        }
    }
}