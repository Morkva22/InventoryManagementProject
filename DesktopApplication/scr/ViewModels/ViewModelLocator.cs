using DesktopApplication.Services;

namespace DesktopApplication.scr.ViewModels
{
    public static class ViewModelLocator
    {
        public static ISupabaseClientService? SupaRepository { get; set; }
        public static IWindowService? WindowService { get; set; }
    }
}