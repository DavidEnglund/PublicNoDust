using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class WeatherPane : AccordionPane
	{

		//init weather button group
		SelectButtonGroup weatherButtonGroup = new SelectButtonGroup();

		public WeatherPane() : base("Weather", "unselectedNone.png")
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

			// set the option enum
			App.WeatherOption = WeatherOptions.RainExpected;

			if (!Owner.IsPaneVisited(Owner.Panes["LocationArea"]))
			{
				Owner.VisitPane(Owner.Panes["LocationArea"]);
			}
        }

		//sunny area button click
		public void sunButton_clicked(object sender, EventArgs e)
		{
			weatherAnswer.Text = "No, I do not expect rain in the area";
			Title = "No Rain Expected";
			Image = "accordion_icon_weather_sun.png";

			// set the option enums
			App.WeatherOption = WeatherOptions.NoRainExpected;

			if (!Owner.IsPaneVisited(Owner.Panes["LocationArea"]))
			{
				Owner.VisitPane(Owner.Panes["LocationArea"]);
			}
        }
	}
}

