using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.ViewModels;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.Views
{
    public partial class AddJuridicalDialog : Window
    {
        public LegalContactData? Result { get; private set; }
    
        public AddJuridicalDialog()
        {
            InitializeComponent();
            DataContext = new AddJuridicalViewModel(this);
        }

        public void SetResult(LegalContactData result) => Result = result;
    
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}