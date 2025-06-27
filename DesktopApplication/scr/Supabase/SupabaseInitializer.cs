using Supabase;

namespace DesktopApplication.Services
{
    public static class SupabaseInitializer
    {
        private static Client _client;
        
        public static Client GetClient(string url, string key)
        {
            if (_client == null)
            {
                var options = new SupabaseOptions { AutoConnectRealtime = true };
                _client = new Client(url, key, options);
            }
            return _client;
        }
    }
}