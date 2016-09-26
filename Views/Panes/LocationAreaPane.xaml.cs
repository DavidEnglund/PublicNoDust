using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class LocationAreaPane : AccordionPane
	{
		public LocationAreaPane() : base("Location and Area", "accordion_icon_location.png")
        {
            InitializeComponent();
			Header.SetDynamicResource(StyleProperty, "locationAccordionStyle");
        }
    }
}

