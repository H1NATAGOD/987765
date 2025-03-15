// Views/Dialogs/AddPhysicalDialog.axaml.cs
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.ViewModels;
using AvaloniaApplication2.ViewModels.Dialogs;

namespace AvaloniaApplication2.Views.Dialogs
{
  
    
    // Views/Dialogs/AddPhysicalDialog.axaml.cs
    public partial class AddPhysicalDialog : Window
    {
        public PhysicalContactData? Result { get; private set; }
    
        public AddPhysicalDialog()
        {
            InitializeComponent();
            DataContext = new AddPhysicalViewModel(this);
        }

        public void SetResult(PhysicalContactData result) => Result = result;
    
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
    
}