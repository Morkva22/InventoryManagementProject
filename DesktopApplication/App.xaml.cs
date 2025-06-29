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
            const string url = "your supabase-url"; // Replace with your Supabase URL
            const string key = "your supabase-key"; // Replace with your Supabase Key
            var client = new Client(url, key, options);
            await client.InitializeAsync();

            return client;
        }
        

        private IServiceProvider ConfigureServices(Client client)
        {
            var services = new ServiceCollection();

            // Register Supabase client
            services.AddSingleton(client);
            
            // Register services
            services.AddSingleton<ISupabaseClientService, SupabaseClientService>();
            services.AddSingleton<SupabaseClientService>(); // For backward compatibility
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // ViewModels - removing IWindowService dependency for ProductViewModel
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

            // Main window
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
            var errorMessage = $"Startup error: {ex.Message}";
            
            if (ex.InnerException != null)
            {
                errorMessage += $"\nInner error: {ex.InnerException.Message}";
            }

            // Console logging for debugging
            System.Diagnostics.Debug.WriteLine($"Full startup error:");
            System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                System.Diagnostics.Debug.WriteLine($"InnerException: {ex.InnerException}");
            }

            Console.WriteLine($"Startup error: {ex}");
            
            MessageBox.Show(errorMessage, "Application startup error", 
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
                System.Diagnostics.Debug.WriteLine($"Exit error: {ex.Message}");
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}