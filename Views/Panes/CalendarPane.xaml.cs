using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class CalendarPane : AccordionPane
	{

		//init calendar button group
		SelectButtonGroup calendarButtonGroup = new SelectButtonGroup();
        SelectImageButton under30Button = new SelectImageButton();
        SelectImageButton over30Under180Button = new SelectImageButton();
        SelectImageButton over180Button = new SelectImageButton();

        public CalendarPane() : base("Duration", "unselectedNone.png")
		{
			InitializeComponent();
            createButtons();

            Header.SetDynamicResource(StyleProperty, "calendarAccordionStyle");

			//add calendar buttons to button group
			calendarButtonGroup.AddButton(under30Button);
			calendarButtonGroup.AddButton(over30Under180Button);
			calendarButtonGroup.AddButton(over180Button);
		}

		public override void OnPaneExpanded()
		{
            //Recreate buttons
            removeButtons();
            createButtons();

			//change over30Under180 button icon (unslected and selected icon)
			over30Under180Button.SelectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_over30.png" : "choice_calendar_under180.png";
			over30Under180Button.UnselectedImage = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "choice_calendar_over30.png" : "choice_calendar_under180.png";

			if (App.DurationOption == DurationOptions.None && Owner.IsPaneVisited(((AccordionViewModel)BindingContext).CalendarPane))
			{
				Image = "unselectedNone.png";
				calendarButtonGroup.Selected = null;
			}
		}


        private void createButtons()
        {
            double sizePercentage = 0;

            setSharedProperties(under30Button, over30Under180Button, over180Button);

            //under 30 days button
            under30Button.SelectedImage = "choice_calendar_under30.png";
            under30Button.UnselectedImage = "choice_calendar_under30.png";
            under30Button.Clicked += under30Button_clicked;

            //under 180 days button
            over30Under180Button.SelectedImage = "choice_calendar_over30.png";
            over30Under180Button.UnselectedImage = "choice_calendar_over30.png";
            over30Under180Button.Clicked += over30Under180Button_clicked;

            //over 180 days button
            over180Button.SelectedImage = "choice_calendar_over180.png";
            over180Button.UnselectedImage = "choice_calendar_over180.png";
            over180Button.Clicked += over180Button_clicked;
            over180Button.IsVisible = (App.TrafficOption == TrafficOptions.TraffickedArea) ? false : true;  //trafficked choice does not display over 180 days choice


            //Adding the buttons to the layout
            //ConstraintX, ConstraintY, ConstraintWidth, ConstraintHeight

            if (over180Button.IsVisible)    //if all three options are visible
            {
                sizePercentage = 0.28;   // 3 buttons - intentionnally not 1/3

                //under 180 days button
                layoutDurationButtons.Children.Add(over30Under180Button,
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
                layoutDurationButtons.Children.Add(over30Under180Button,
                    Constraint.RelativeToParent((parent) => { return parent.Width - parent.Width * sizePercentage - 20; }),     // separated by 20 pixels from the end of the screen
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                    Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                    );
            }
            //under 30 days button
            layoutDurationButtons.Children.Add(under30Button,
                Constraint.Constant(20),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); }),
                Constraint.RelativeToParent((parent) => { return (parent.Width * sizePercentage); })
                );
            //over 180 days button
            layoutDurationButtons.Children.Add(over180Button,
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

                btn.Padding = 26;     //Doesn't seem to affect anything but was in the original code
            }
        }

        private void removeButtons()
        {
            layoutDurationButtons.Children.Remove(under30Button);
            layoutDurationButtons.Children.Remove(over30Under180Button);
            layoutDurationButtons.Children.Remove(over180Button);
        }


		//under 30 button click
		public void under30Button_clicked(object sender, EventArgs e)
		{
			calendarAnswer.Text = "Solution is for under 30 days";
			Title = "Under 30 Days";
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
			//switches between 30 and 180 options depending on traffic option choice
			calendarAnswer.Text = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Solution is for over 30 days" : "Solution is for under 180 days";
			Title = (App.TrafficOption == TrafficOptions.TraffickedArea) ? "Over 30 Days" : "Under 180 Days";
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
			calendarAnswer.Text = "Solution is for over 180 days";
			Title = "Over 180 Days";
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

