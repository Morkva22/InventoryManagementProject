using System.Windows.Controls;
using DesktopApplication.ViewModels;

namespace DesktopApplication.Views
{
    public partial class ProductsUserControl : UserControl
    {
        public ProductsUserControl(ProductViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Загрузить продукты при инициализации
            viewModel.LoadProductsCommand.Execute(null);
        }
    }
}