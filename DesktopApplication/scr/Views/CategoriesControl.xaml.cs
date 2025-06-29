using System.Windows.Controls;
using System.Windows.Input;
using DesktopApplication.ViewModels;

namespace DesktopApplication.Views
{
    public partial class CategoriesControl : UserControl
    {
        public CategoriesControl(CategoryViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CategoryViewModel viewModel && viewModel.EditCategoryCommand.CanExecute(null))
            {
                viewModel.EditCategoryCommand.Execute(null);
            }
        }
    }
}