using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class AccordionPage : ContentPage
	{
		public AccordionPage()
		{
			InitializeComponent();

			Accordion.BindingContext = new AccordionViewModel();

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

