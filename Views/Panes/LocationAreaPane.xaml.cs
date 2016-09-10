using Dustbuster.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class LocationAreaPane : AccordionPane
	{
		public LocationAreaPane() : base("Location and Area")
        {
            BindingContext = new AreaViewModel();
            InitializeComponent(); 
        }
	}
}

