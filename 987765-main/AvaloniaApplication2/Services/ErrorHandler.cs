using System.Threading.Tasks;
using Avalonia.Controls;

namespace AvaloniaApplication2.Services
{
    public static class ErrorHandler
    {
        public static async Task ShowErrorDialog(string title, string message, Window parent)
        {
            var dialog = new Window
            {
                Title = title,
                Content = new TextBlock { Text = message },
                SizeToContent = SizeToContent.WidthAndHeight
            };
            
            await dialog.ShowDialog(parent);
        }
    }
}