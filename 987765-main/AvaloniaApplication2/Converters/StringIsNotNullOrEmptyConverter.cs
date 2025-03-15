﻿using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaApplication2.Utils;

// Converters/StringIsNotNullOrEmptyConverter.cs
public class StringIsNotNullOrEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !string.IsNullOrEmpty(value as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}