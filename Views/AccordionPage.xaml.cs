using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class AccordionPage : ContentPage
	{
		public AccordionPage()
		{
			BindingContext = new AccordionViewModel();

			InitializeComponent();
		}
	}
}

