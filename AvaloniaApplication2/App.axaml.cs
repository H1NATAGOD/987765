using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Services;
using AvaloniaApplication2.Services.Storage;
using AvaloniaApplication2.ViewModels;
using AvaloniaApplication2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication2;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection()
            .AddDbContext<AppDbContext>()
            .AddSingleton<AesService>()
            .AddSingleton<PersonalBookService>() // Добавлен новый сервис
            .BuildServiceProvider();

        // 3. Обновите конструктор ViewModel
        DataContext = new UserMainViewModel(
            services.GetRequiredService<AppDbContext>(),
            services.GetRequiredService<AesService>(),
            services.GetRequiredService<PersonalBookService>() // Добавлен третий аргумент
        );

        base.OnFrameworkInitializationCompleted();
    }
    
}