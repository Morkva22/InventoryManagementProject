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
    public partial class CategoryViewModel : ObservableObject
    {
        private readonly ISupabaseClientService _repository;

        public ObservableCollection<CategoryModel> Categories { get; } = new();

        [ObservableProperty]
        private CategoryModel? selectedCategory;

        [ObservableProperty]
        private string searchText = "";

        public ObservableCollection<CategoryModel> FilteredCategories { get; } = new();

        public ICommand LoadCategoriesCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand SearchCommand { get; }

        public CategoryViewModel(ISupabaseClientService repository)
        {
            _repository = repository;

            LoadCategoriesCommand = new AsyncRelayCommand(LoadCategories);
            AddCategoryCommand = new AsyncRelayCommand(AddCategory);
            EditCategoryCommand = new AsyncRelayCommand(EditCategory, () => SelectedCategory != null);
            DeleteCategoryCommand = new AsyncRelayCommand(DeleteCategory, () => SelectedCategory != null);
            SearchCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(ApplyFilter);

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SearchText))
                    ApplyFilter();
            };

            _ = LoadCategories();
        }

        private async Task LoadCategories()
        {
            try
            {
                var categories = await _repository.GetAllCategories();
                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
                ApplyFilter();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddCategory()
        {
            var newCategory = CategoryModel.CreateDefault();
            var editWindow = new CategoryEditWindow(newCategory);

            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    if (editWindow.Category.IsValid(out string errorMessage))
                    {
                        await _repository.AddCategory(editWindow.Category);
                        await LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task EditCategory()
        {
            if (SelectedCategory == null) return;

            var editWindow = new CategoryEditWindow(SelectedCategory);

            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    if (editWindow.Category.IsValid(out string errorMessage))
                    {
                        await _repository.UpdateCategory(editWindow.Category);
                        await LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error updating category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task DeleteCategory()
        {
            if (SelectedCategory == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete category '{SelectedCategory.Name}'?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _repository.DeleteCategory(SelectedCategory.Id);
                    await LoadCategories();
                    SelectedCategory = null;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplyFilter()
        {
            FilteredCategories.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Categories
                : Categories.Where(c => c.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

            foreach (var category in filtered)
            {
                FilteredCategories.Add(category);
            }
        }

        partial void OnSelectedCategoryChanged(CategoryModel? value)
        {
            ((AsyncRelayCommand)EditCategoryCommand).NotifyCanExecuteChanged();
            ((AsyncRelayCommand)DeleteCategoryCommand).NotifyCanExecuteChanged();
        }
    }
}