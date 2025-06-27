using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Supabase;
using DesktopApplication.Services;
using DesktopApplication.ViewModels;
using DesktopApplication.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace DesktopApplication
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                var client = await InitializeSupabase();
                ServiceProvider = ConfigureServices(client);
                ConfigureNavigation();
                
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                HandleStartupError(ex);
                Shutdown(1);
            }
        }

        private async Task<Client> InitializeSupabase()
        {
            const string url = "  https://your-supabase-url.supabase.co ";
            const string key = "your-anon-key  ";
            
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true,
                AutoRefreshToken = true
            };
            
            var client = new Client(url, key, options);
            await client.InitializeAsync();
            
            return client;
        }

        private IServiceProvider ConfigureServices(Client client)
        {
            var services = new ServiceCollection();
    
            // Регистрация клиента как конкретного типа Client
            services.AddSingleton(client);
            services.AddSingleton<ISupabaseClientService, SupabaseClientService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
    
            // ViewModels
            services.AddTransient<ProductViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<SupplierViewModel>();
            services.AddTransient<HomeViewModel>();
    
            // Views
            services.AddTransient<MainMenuControl>();
            services.AddTransient<ProductsUserControl>();
            services.AddTransient<CategoriesControl>();
            services.AddTransient<SuppliersControl>();
            services.AddTransient<HomeUserControl>();
    
            // Главное окно
            services.AddSingleton<MainWindow>();
    
            return services.BuildServiceProvider();
        }

        private void ConfigureNavigation()
        {
            var navService = ServiceProvider.GetRequiredService<INavigationService>();
            
            navService.RegisterView<ProductsUserControl>("Products");
            navService.RegisterView<CategoriesControl>("Categories");
            navService.RegisterView<SuppliersControl>("Suppliers");
            navService.RegisterView<HomeUserControl>("Home");
        }

        private void HandleStartupError(Exception ex)
        {
            Console.WriteLine($"Ошибка запуска: {ex}");
            MessageBox.Show("Не удалось запустить приложение", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            (ServiceProvider as IDisposable)?.Dispose();
            base.OnExit(e);
        }
    }
}