using DesktopApplication.ViewModels;
using System.Windows.Controls;

namespace DesktopApplication.Views
{
    public partial class SuppliersControl : UserControl
    {
        public SuppliersControl(SupplierViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            
            // Загружаем данные при инициализации
            Loaded += async (s, e) =>
            {
                if (viewModel.LoadSuppliersCommand.CanExecute(null))
                {
                    await ((CommunityToolkit.Mvvm.Input.AsyncRelayCommand)viewModel.LoadSuppliersCommand).ExecuteAsync(null);
                }
            };
        }
    }
}