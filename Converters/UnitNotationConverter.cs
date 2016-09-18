using System;
using Xamarin.Forms;
using System.Globalization;

namespace Dustbuster
{
	public class UnitNotationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Units)
			{
				bool squared = (parameter != null) && System.Convert.ToBoolean(parameter);

				switch ((Units)value)
				{
					case Units.Kilometre:
						return (!squared) ? "km" : "km²";
					case Units.Metre:
						return (!squared) ? "m" : "m²";

				}
			}

			return value;
		}


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
