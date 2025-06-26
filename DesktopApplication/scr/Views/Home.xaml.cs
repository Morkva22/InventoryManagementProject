using DesktopApplication.ViewModels;
using System.Windows.Controls;

namespace DesktopApplication.Views;

public partial class HomeUserControl : UserControl
{
    public HomeUserControl(HomeViewModels viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}