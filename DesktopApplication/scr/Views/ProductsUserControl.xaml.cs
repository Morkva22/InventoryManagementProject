using DesktopApplication.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace DesktopApplication.Views
{
    public partial class ProductsUserControl : UserControl
    {
        public ProductsUserControl(ProductViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}