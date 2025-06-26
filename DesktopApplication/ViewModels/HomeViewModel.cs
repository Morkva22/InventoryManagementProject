using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace DesktopApplication.ViewModels;

public class HomeViewModels
{
    public ICommand HelloCommand { get; }
    public ICommand HelloCommandAsync { get; }
    public HomeViewModels()
    {
        HelloCommand = new RelayCommand(ExecuteHelloCommand);
        HelloCommandAsync = new AsyncRelayCommand(ExecuteHelloCommandAsync);
    }

    public void ExecuteHelloCommand()
    {
        MessageBox.Show("Hello World!");
    }
    
    public async Task ExecuteHelloCommandAsync()
    {
        await Task.Run(() => MessageBox.Show("Hello World Async!"));
    }
}