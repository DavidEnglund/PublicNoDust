using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class LocationAreaPane : AccordionPane
	{
		public LocationAreaPane() : base("Location and Area")
        {
			InitializeComponent();
		}

		public void OnBackButtonClicked(object sender, EventArgs args)
		{
			Navigation.PushAsync(new DustbusterPage());
		}
	}
}

