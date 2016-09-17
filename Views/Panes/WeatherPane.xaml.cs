using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class WeatherPane : AccordionPane
	{

		//init weather button group
		SelectButtonGroup weatherButtonGroup = new SelectButtonGroup();

		public WeatherPane() : base("Weather", "accordion_icon_weather_sun.png")
		{
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "weatherAccordionStyle");

			//add rain and sun buttons to button group
			weatherButtonGroup.AddButton(rainButton);
			weatherButtonGroup.AddButton(sunButton);
		}

		//rainy area button click
		public void rainButton_clicked(object sender, EventArgs e)
		{
			weatherAnswer.Text = "Yes, I expect rain in the area";
			Title = "Rain Expected";
			Image = "accordion_icon_weather_rain.png";

			if (!Owner.IsPaneVisited(Owner.Panes["Calendar"]))
			{
				Owner.VisitPane(Owner.Panes["Calendar"]);
			}
            // set the option enum - david
            App.WeatherOption  = WeatherOptions.RainExpected;
        }

		//sunny area button click
		public void sunButton_clicked(object sender, EventArgs e)
		{
			weatherAnswer.Text = "No, I do not expect rain in the area";
			Title = "No Rain Expected";
			Image = "accordion_icon_weather_sun.png";

			if (!Owner.IsPaneVisited(Owner.Panes["Calendar"]))
			{
				Owner.VisitPane(Owner.Panes["Calendar"]);
			}
            // set the option enum - david
            App.WeatherOption = WeatherOptions.NoRainExpected;
        }
	}
}

