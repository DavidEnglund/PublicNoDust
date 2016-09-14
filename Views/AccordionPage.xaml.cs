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

