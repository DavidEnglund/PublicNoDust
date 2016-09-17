using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{

		//init calendar button group
		SelectButtonGroup calendarButtonGroup = new SelectButtonGroup();

		public CalendarPane() : base("Duration", "accordion_icon_calendar_none.png")
        {
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "calendarAccordionStyle");

			//add calendar buttons to button group
			calendarButtonGroup.AddButton(under30Button);
			calendarButtonGroup.AddButton(over30Under180Button);
			calendarButtonGroup.AddButton(over180Button);

			checkButtonOptions();
		}

		public void checkButtonOptions()
		{
			//trafficked choice, does not display over 180 day choice
			over180Button.IsVisible = (App.TrafficOption == TrafficOptions.TraffickedArea) ? true : false;
			//change over30Under180 button icon (unslected and selected icon)
			over30Under180Button.SelectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_under180.png" : "choice_calendar_over30.png";
			over30Under180Button.UnselectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_under180.png" : "choice_calendar_over30.png";
		}

		//under 30 button click
		public void under30Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for under 30 days";
			Title = "Under 30 Days";
			Image = "accordion_icon_calendar_under30.png";
			//set the option enum
			App.DurationOption = DurationOptions.Under1Month;
			//Trafficked Areas always goto LocationArea, Non Trafficked under 30 goes to weather
			(App.TrafficOption == TrafficOptions.TraffickedArea ? (Action)goToLocationAreaPane : goToWeatherPane)();
        }

		//over 30 or under 180 button click
		public void over30Under180Button_clicked(object sender, EventArgs e)
		{
			//switches between 30 and 180 options depending on traffic option choice
			calendarAnswer.Text = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Solution is for over 30 days" : "Solution is for under 180 days";
			Title = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Over 30 Days" : "Under 180 Days";
			Image = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "accordion_icon_calendar_over30.png": "accordion_icon_calendar_under180.png";
			App.DurationOption = (App.TrafficOption == TrafficOptions.TraffickedArea) ? DurationOptions.Over1Month : DurationOptions.Under3Months;
			//Trafficked Areas always goto LocationArea, Non Trafficked under 180 goes to weather
			(App.TrafficOption == TrafficOptions.TraffickedArea ? (Action)goToLocationAreaPane : goToWeatherPane)();
        }

		//over 180 button click
		public void over180Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for over 180 days";
			Title = "Over 180 Days";
			Image = "accordion_icon_calendar_over180.png";
			// set the option enum
			App.DurationOption = DurationOptions.Over3Months;
			//over 180 days always goesto location area
			goToLocationAreaPane();
        }

		private void goToWeatherPane()
		{
			//Goto weather pane
			if (!Owner.IsPaneVisited(Owner.Panes["Weather"]))
			{
				Owner.VisitPane(Owner.Panes["Weather"]);
			}
		}

		private void goToLocationAreaPane()
		{
			//Goto location area pane
			if (!Owner.IsPaneVisited(Owner.Panes["LocationArea"]))
			{
				Owner.VisitPane(Owner.Panes["LocationArea"]);
			}
		}
	}
}

