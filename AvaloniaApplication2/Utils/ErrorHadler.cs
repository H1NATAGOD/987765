using System;
using System.Linq.Expressions;
using AvaloniaApplication2.Utils;
using Serilog;
using System.Windows;
namespace AvaloniaApplication2.Utils;

public class ErrorHadler
{
    public static void Hadler(Exception ex)
    {
        Log.Error(ex, "Critical error");
        MessageBox.Show($"Ошибка: {ex.Message}");
    }
    
    
}

try
{
    throw new InvalidOperationException("Неизвестная ошибка");
}

catch(Exception ex)
{
    ErrorHadler.Hadler(ex);
}