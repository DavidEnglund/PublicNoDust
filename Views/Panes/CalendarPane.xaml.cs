using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{
		public CalendarPane() : base("Duration", "accordion_icon_calendar_over180.png")
        {
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "calendarAccordionStyle");

		}

		public void OnBackButtonClicked(object sender, EventArgs args)
		{
			Navigation.PushAsync(new DustbusterPage());
		}
	}
}

