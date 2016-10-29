using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{

		//init calendar button group
		SelectButtonGroup calendarButtonGroup = new SelectButtonGroup();

        public CalendarPane() : base("Duration", "unselectedNone.png")
		{
			InitializeComponent();
            createButtons();

            Header.SetDynamicResource(StyleProperty, "calendarAccordionStyle");

			//add calendar buttons to button group
			calendarButtonGroup.AddButton(btnUnder30);
			calendarButtonGroup.AddButton(btnOver30Under180);
			calendarButtonGroup.AddButton(btnOver180);
		}

		public override void OnPaneExpanded()
		{
            //Recreate buttons
            removeButtons();
            createButtons();

			//change over30Under180 button icon (unslected and selected icon)
			btnOver30Under180.SelectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_over_1_month.png" : "choice_calendar_1_to_6_months.png";
			btnOver30Under180.UnselectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_over_1_month.png" : "choice_calendar_1_to_6_months.png";

			if (App.DurationOption == DurationOptions.None && Owner.IsPaneVisited(((AccordionViewModel)BindingContext).CalendarPane))
			{
				Image = "unselectedNone.png";
				calendarButtonGroup.Selected = null;
			}
		}


        private void createButtons()
        {
            double sizePercentage = 0;

            setSharedProperties(btnUnder30, btnOver30Under180, btnOver180);

            //under 30 days button
            btnUnder30.SelectedImage = "choice_calendar_1_month.png";
            btnUnder30.UnselectedImage = "choice_calendar_1_month.png";
            btnUnder30.Clicked += under30Button_clicked;

            //under 180 days button
            btnOver30Under180.SelectedImage = "choice_calendar_1_to_6_months.png";
            btnOver30Under180.UnselectedImage = "choice_calendar_1_to_6_months.png";
            btnOver30Under180.Clicked += over30Under180Button_clicked;

            //over 180 days button
            btnOver180.SelectedImage = "choice_calendar_over_6_months.png";
            btnOver180.UnselectedImage = "choice_calendar_over_6_months.png";
            btnOver180.Clicked += over180Button_clicked;
            btnOver180.IsVisible = (App.TrafficOption == TrafficOptions.TraffickedArea) ? false : true;  //trafficked choice does not display over 180 days choice


            //Adding the buttons to the layout
            //ConstraintX, ConstraintY, ConstraintWidth, ConstraintHeight

            if (btnOver180.IsVisible)    //if all three options are visible
            {
                sizePercentage = 0.28;   // 3 buttons - intentionnally not 1/3

                //under 180 days button
                layoutDurationButtons.Children.Add(btnOver30Under180,
                    Constraint.RelativeToParent((parent) => { return parent.Width * 0.5 - parent.Width * sizePercentage / 2; }),    // Centered
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                    );
            }
            else
            {
                sizePercentage = 0.4;   // 2 buttons - intentionnally not 1/2

                //under 180 days button
                layoutDurationButtons.Children.Add(btnOver30Under180,
                    Constraint.RelativeToParent((parent) => { return parent.Width - parent.Width * sizePercentage - 20; }),     // separated by 20 pixels from the end of the screen
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                    );
            }
            //under 30 days button
            layoutDurationButtons.Children.Add(btnUnder30,
                Constraint.Constant(20),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                );
            //over 180 days button
            layoutDurationButtons.Children.Add(btnOver180,
                Constraint.RelativeToParent((parent) => { return parent.Width - parent.Width * sizePercentage - 20; }),    // separated by 20 pixels from the end of the screen
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                );
        }

        private void setSharedProperties(params SelectImageButton[] buttons)
        {
            //Defines the appearance of the buttons.
            foreach (SelectImageButton btn in buttons)
            {
                btn.SelectedBorderColor = Color.Black;
                btn.SelectedBorderWidth = 0;

                btn.HoldBorderWidth = 0;
                btn.HoldBorderColor = Color.Black;

                btn.UnselectedBorderColor = Color.White;
                btn.UnselectedBorderWidth = 0;

                btn.SetDynamicResource(VisualElement.StyleProperty, "selectableButtonStyle");
            }
        }

        private void removeButtons()
        {
            layoutDurationButtons.Children.Remove(btnUnder30);
            layoutDurationButtons.Children.Remove(btnOver30Under180);
            layoutDurationButtons.Children.Remove(btnOver180);
        }


		//under 30 button click
		public void under30Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for under 1 month";
			Title = "Under 1 Month";
			Image = "accordion_icon_calendar_under30.png";
			//set the option enum
			App.DurationOption = DurationOptions.Under1Month;
			if (App.TrafficOption == TrafficOptions.TraffickedArea) { App.WeatherOption = WeatherOptions.None; }
			//Trafficked Areas always goto LocationArea, Non Trafficked under 30 goes to weather
			(App.TrafficOption == TrafficOptions.TraffickedArea ? (Action)goToLocationAreaPane : goToWeatherPane)();
        }

		//over 30 or under 180 button click
		public void over30Under180Button_clicked(object sender, EventArgs e)
		{
			//switches between 1+ and 1-6 options depending on traffic option choice
			calendarAnswer.Text = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Solution is for over 1 month" : "Solution is for under 6 months";
			Title = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Over 1 Month" : "Under 6 Months";
			Image = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "accordion_icon_calendar_over30.png": "accordion_icon_calendar_under180.png";
			App.DurationOption = (App.TrafficOption == TrafficOptions.TraffickedArea) ? DurationOptions.Over1Month : DurationOptions.Under6Months;
			//if traffic option is selected weather option is none
			if (App.TrafficOption == TrafficOptions.TraffickedArea) { App.WeatherOption = WeatherOptions.None; }
			//Trafficked Areas always goto LocationArea, Non Trafficked under 180 goes to weather
			(App.TrafficOption == TrafficOptions.TraffickedArea ? (Action)goToLocationAreaPane : goToWeatherPane)();
        }

		//over 180 button click
		public void over180Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for over 6 months";
			Title = "Over 6 Months";
			Image = "accordion_icon_calendar_over180.png";
			// set the option enum
			App.DurationOption = DurationOptions.Over6Months;
			App.WeatherOption = WeatherOptions.None;
			//over 180 days always goesto location area
			goToLocationAreaPane();
        }


		private void goToWeatherPane()
		{
			//Goto weather pane
			if (!Owner.IsPaneVisited(((AccordionViewModel)BindingContext).WeatherPane))
			{
				Owner.VisitPane(((AccordionViewModel)BindingContext).LocationAreaPane, ((AccordionViewModel)BindingContext).WeatherPane);
			}
		}

		private void goToLocationAreaPane()
		{

			//Goto location area pane
			if (!Owner.IsPaneVisited(((AccordionViewModel)BindingContext).LocationAreaPane) || App.DurationOption == DurationOptions.Over6Months)
			{
				Owner.VisitPane(((AccordionViewModel)BindingContext).WeatherPane, ((AccordionViewModel)BindingContext).LocationAreaPane);
			}
		}
	}
}

