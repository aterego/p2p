using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace p2p.Helpers
{
    public class LevelToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (int)value;

            // return new Thickness(System.Convert.ToDouble(v), 0, 0, 0);
            //  return String.Concat(Enumerable.Repeat("@", v));
            if (v > 0)
            {
                return "   ";
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value is Thickness ? (Thickness)value : new Thickness();
            return t.Left / 5;
        }
    }
}
