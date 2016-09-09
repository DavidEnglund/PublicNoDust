using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{
		public CalendarPane() : base("Duration")
        {
			InitializeComponent();
		}

		public void OnBackButtonClicked(object sender, EventArgs args)
		{
			Navigation.PushAsync(new DustbusterPage());
		}
	}
}

