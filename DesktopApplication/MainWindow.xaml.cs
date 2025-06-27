using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Views;
using ManagementSystem;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopApplication
{
    public partial class MainWindow : Window
    {
        private readonly ISupabaseRepository _repository;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация сервисов
            var serviceProvider = ConfigureServices();
            _repository = serviceProvider.GetRequiredService<ISupabaseRepository>();

            // Инициализация Supabase
            _repository.InitDatabase().GetAwaiter().GetResult();

            // Добавление главного меню
            var mainMenu = new MainMenuControl(_repository);
            ((Grid)Content).Children.Add(mainMenu);
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрация сервисов
            services.AddSingleton<ISupabaseRepository>(sp =>
                new SupabaseRepository(
                    


            return services.BuildServiceProvider();
        }
    }
}