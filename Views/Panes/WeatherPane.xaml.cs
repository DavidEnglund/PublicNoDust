using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class WeatherPane : AccordionPane
	{
		public WeatherPane() : base("Weather")
		{
			InitializeComponent();
		}

		public void OnCalendarButtonClicked(object sender, EventArgs args)
		{
			Owner.VisitPane(Owner.Panes["Calendar"]);
		}
	}
}

