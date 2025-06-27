// CategoryViewModel.cs - исправленный
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class CategoryViewModel
    {
        private readonly SupabaseClientService _repository;

        public ObservableCollection<CategoryModel> Categories { get; } = new();
        public CategoryModel SelectedCategory { get; set; }

        public ICommand LoadCategoriesCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand UpdateCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public CategoryViewModel(SupabaseClientService repository)
        {
            _repository = repository;
            
            LoadCategoriesCommand = new AsyncRelayCommand(LoadCategories);
            AddCategoryCommand = new AsyncRelayCommand(AddCategory);
            UpdateCategoryCommand = new AsyncRelayCommand(UpdateCategory);
            DeleteCategoryCommand = new AsyncRelayCommand(DeleteCategory);
        }

        private async Task LoadCategories()
        {
            var categories = await _repository.GetAllCategories();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        private async Task AddCategory()
        {
            var newCategory = new CategoryModel { Name = "Новая категория" };
            await _repository.AddCategory(newCategory);
            await LoadCategories();
        }

        private async Task UpdateCategory()
        {
            if (SelectedCategory != null)
            {
                await _repository.UpdateCategory(SelectedCategory);
                await LoadCategories();
            }
        }

        private async Task DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                await _repository.DeleteCategory(SelectedCategory.Id);
                await LoadCategories();
            }
        }
    }
}
