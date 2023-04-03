using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddictionApp.Tools
{
    public class DateTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var cultureBr = new System.Globalization.CultureInfo("pt-BR");
                var res = DateTime.Parse(value as string);
                return res.Day + "\n" + cultureBr.DateTimeFormat.GetAbbreviatedDayName(res.DayOfWeek);
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
