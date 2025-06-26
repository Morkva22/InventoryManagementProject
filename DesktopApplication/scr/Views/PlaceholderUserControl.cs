using System.Windows.Controls;
using System.Windows;

namespace DesktopApplication.Views
{
    public class PlaceholderUserControl : UserControl
    {
        public PlaceholderUserControl(string sectionName)
        {
            var textBlock = new TextBlock
            {
                Text = $"Здесь будет раздел: {sectionName}",
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Content = textBlock;
        }
    }
}