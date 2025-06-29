namespace DesktopApplication.Messages
{
    public class NavigationMessage
    {
        public string ViewName { get; }

        public NavigationMessage(string viewName)
        {
            ViewName = viewName;
        }
    }
}