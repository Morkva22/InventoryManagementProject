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

            // Регистрация Supabase клиента
            services.AddSingleton(client);
            
            // Регистрация сервисов
            services.AddSingleton<ISupabaseClientService, SupabaseClientService>();
            services.AddSingleton<SupabaseClientService>(); // Для обратной совместимости
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
            var errorMessage = $"Ошибка запуска: {ex.Message}";
            
            if (ex.InnerException != null)
            {
                errorMessage += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
            }

            // Логирование в консоль для отладки
            System.Diagnostics.Debug.WriteLine($"Полная ошибка запуска:");
            System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                System.Diagnostics.Debug.WriteLine($"InnerException: {ex.InnerException}");
            }

            Console.WriteLine($"Ошибка запуска: {ex}");
            
            MessageBox.Show(errorMessage, "Ошибка запуска приложения", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                (ServiceProvider as IDisposable)?.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при закрытии: {ex.Message}");
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}