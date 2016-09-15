using Dustbuster;
using Dustbuster.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class DustbusterPage : ContentPage
	{
        private bool panelShowing = false;
        private DustbusterViewModel dustbusterViewModel;

        public DustbusterPage()
		{
            dustbusterViewModel = new DustbusterViewModel();
            BindingContext = dustbusterViewModel;

            InitializeComponent();
        }


        private async void AnimatePanel()
        {
            // negate the state
            this.panelShowing = !panelShowing;

            if (this.panelShowing)
            {
                btnDarkBackground.IsVisible = true;

                foreach (var child in panel.Children)
                {
                    child.Scale = 0;
                }

                var rect = new Rectangle(0, panel.Y, panel.Width, panel.Height);
                await this.panel.LayoutTo(rect, 250, Easing.CubicIn);

                foreach (var child in panel.Children)
                {
                    await child.ScaleTo(1.2, 50, Easing.CubicIn);
                    await child.ScaleTo(1, 50, Easing.CubicOut);
                }
            }
            else
            {
                var rect = new Rectangle(-(panel.Width), panel.Y, panel.Width, panel.Height);
                await this.panel.LayoutTo(rect, 200, Easing.CubicOut);

                foreach (var child in panel.Children)
                {
                    child.Scale = 0;
                }

                btnDarkBackground.IsVisible = false;
            }
        }

        
        private void btnSideMenu_Clicked(object sender, EventArgs e)
        {
            AnimatePanel();
        }

        private void listViewItem_Clicked(object sender, EventArgs e)
        {
            AnimatePanel();

            //TODO  Make it display the corresponding result
        }

        private async void btnSettings_Clicked(object sender, EventArgs e)
        {
            AnimatePanel();
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void btnAbout_Clicked(object sender, EventArgs e)
        {
            AnimatePanel();
            await Navigation.PushAsync(new AboutPage());
        }


        private async void TapCivilButton(object sender, EventArgs e)
        {
			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["civilTrafficAccordionStyle"];
			App.Current.Resources["calendarAccordionStyle"] = App.Current.Resources["civilCalendarAccordionStyle"];
			App.Current.Resources["locationAccordionStyle"] = App.Current.Resources["civilLocationAccordionStyle"];
			App.Current.Resources["weatherAccordionStyle"] = App.Current.Resources["civilWeatherAccordionStyle"];

            await Navigation.PushAsync(new AccordionPage());
        }

        private async void TapMiningButton(object sender, EventArgs e)
        {
			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["miningTrafficAccordionStyle"];
			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["miningTrafficAccordionStyle"];
			App.Current.Resources["calendarAccordionStyle"] = App.Current.Resources["miningCalendarAccordionStyle"];
			App.Current.Resources["locationAccordionStyle"] = App.Current.Resources["miningLocationAccordionStyle"];
			App.Current.Resources["weatherAccordionStyle"] = App.Current.Resources["miningWeatherAccordionStyle"];

            await Navigation.PushAsync(new AccordionPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

