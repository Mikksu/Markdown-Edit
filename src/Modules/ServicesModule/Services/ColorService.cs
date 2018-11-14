﻿using System;
using System.Windows.Media;
using Infrastructure;

namespace ServicesModule.Services
{
    public class ColorService : IColorService
    {
        public Color FromHtml(string value)
        {
            return (Color?)ColorConverter.ConvertFromString(value) ?? default(Color);
        }

        public Brush CreateBrush(string colorSpec)
        {
            if (string.IsNullOrWhiteSpace(colorSpec)) return null;
            try
            {
                var brush = new SolidColorBrush(FromHtml(colorSpec));
                brush.Freeze();
                return brush;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
    }
}