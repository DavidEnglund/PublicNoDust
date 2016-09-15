using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class WeatherPane : AccordionPane
	{
		public WeatherPane() : base("Weather", "accordion_icon_weather_sun.png")
		{
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "weatherAccordionStyle");
		}

		public void OnCalendarButtonClicked(object sender, EventArgs args)
		{
			Owner.VisitPane(Owner.Panes["Calendar"]);
		}
	}
}

