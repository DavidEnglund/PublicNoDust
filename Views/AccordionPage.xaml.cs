using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class AccordionPage : ContentPage
	{
		private AccordionViewModel viewModel;
		public AccordionPage()
		{
			BindingContext = (viewModel = new AccordionViewModel(Navigation));

			InitializeComponent();

			viewModel.TrafficPane = TrafficPane;
			viewModel.CalendarPane = CalendarPane;
			viewModel.WeatherPane = WeatherPane;
			viewModel.LocationAreaPane = LocationAreaPane;

			//give Title civil or mining string
			Title = (App.IndustryOption == IndustryOptions.Civil) ? "Civil" : "Mining";
           
            if (Settings.EnableReadMode)
            {
                BackgroundColor = Color.White;
            }
            else
            {
                BackgroundImage = "app_title.png";
            }
		}
	}
}

