using Dustbuster.Interfaces;
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

        /**
         * Hide active soft-keyboard when pane dismisses or shrinks
         **/
        public override void OnPaneCollapsed()
        {
            DependencyService.Get<IKeyboardInteraction>().HideKeyboard();
        }
    }
}

