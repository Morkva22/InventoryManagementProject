using System.Windows.Controls;
using System.Windows.Input;
using DesktopApplication.ViewModels;

namespace DesktopApplication.Views
{
    public partial class ProductsUserControl : UserControl
    {
        public ProductsUserControl(ProductViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ProductViewModel viewModel && viewModel.EditProductCommand.CanExecute(null))
            {
                viewModel.EditProductCommand.Execute(null);
            }
        }
    }
}