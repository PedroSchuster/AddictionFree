using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddictionApp.Tools
{

    public class DayHourStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime? time = value as DateTime?;
                TimeSpan? res = DateTime.Now - time;
                return res.Value.Days.ToString() + "d " + res.Value.Hours.ToString() + "h";
            }
            return this;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
