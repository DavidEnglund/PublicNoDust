﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{

		//init calendar button group
		SelectButtonGroup calendarButtonGroup = new SelectButtonGroup();

		public CalendarPane() : base("Duration", "accordion_icon_calendar_over180.png")
        {
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "calendarAccordionStyle");

			//add calendar buttons to button group
			calendarButtonGroup.AddButton(under30Button);
			calendarButtonGroup.AddButton(over30Under180Button);
			calendarButtonGroup.AddButton(over180Button);
		}

		//under 30 button click
		public void under30Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for under 30 days";
			Title = "Under 30 Days";
			Image = "accordion_icon_calendar_under30.png";

			if (!Owner.IsPaneVisited(Owner.Panes["Weather"]))
			{
				Owner.VisitPane(Owner.Panes["Weather"]);
			}
            // set the option enum
            App.DurationOption = DurationOptions.UnderAMonth;
        }

		//over 30 or under 180 button click
		public void over30Under180Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for over 30 days";
			Title = "Over 30 Days";
			Image = "accordion_icon_calendar_over30.png";

			if (!Owner.IsPaneVisited(Owner.Panes["Weather"]))
			{
				Owner.VisitPane(Owner.Panes["Weather"]);
			}
            // set the option enum
            App.DurationOption = DurationOptions.OverAMonth;
        }

		//over 180 button click
		public void over180Button_clicked(object sender, EventArgs e)
		{
			if (App.TrafficOption == TrafficOptions.TraffickedArea)
			{
				over180Button.IsVisible = false;
			}
			else
			{
				calendarAnswer.Text = "Solution is for over 180 days";
				Title = "Over 180 Days";
				Image = "accordion_icon_calendar_over180.png";

				//Over 180 days skips weather
				if (!Owner.IsPaneVisited(Owner.Panes["LocationArea"]))
				{
					Owner.VisitPane(Owner.Panes["LocationArea"]);

				}

				// set the option enum
				App.DurationOption = DurationOptions.OverSixMonths;
			} 
        }
	}
}
