using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2;

public partial class BaseDialog : UserControl
{
    public BaseDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}