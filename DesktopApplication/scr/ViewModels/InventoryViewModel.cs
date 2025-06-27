// InventoryViewModel.cs - исправленный
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class InventoryViewModel
    {
        private readonly ISupabaseClientService _repository;

        public ObservableCollection<InventoryModel> Inventory { get; } = new();
        public InventoryModel SelectedItem { get; set; }

        public ICommand LoadInventoryCommand { get; }
        public ICommand UpdateInventoryCommand { get; }

        public InventoryViewModel(ISupabaseClientService repository)
        {
            _repository = repository;
            
            LoadInventoryCommand = new AsyncRelayCommand(LoadInventory);
            UpdateInventoryCommand = new AsyncRelayCommand(UpdateInventory);
        }

        private async Task LoadInventory()
        {
            var inventory = await _repository.GetAllInventory();
            Inventory.Clear();
            foreach (var item in inventory)
            {
                Inventory.Add(item);
            }
        }

        private async Task UpdateInventory()
        {
            if (SelectedItem != null)
            {
                await _repository.UpdateInventory(SelectedItem);
                await LoadInventory();
            }
        }
    }
}