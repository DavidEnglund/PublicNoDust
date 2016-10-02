using Dustbuster;
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

        private async void btnReadModeButton_Clicked(object sender, EventArgs e)
        {
            if(Settings.EnableReadMode)
            {
                Settings.EnableReadMode = false;
            }
            else
            {
                Settings.EnableReadMode = true;
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
			((NavigationPage)App.Current.MainPage).BarBackgroundColor = Color.FromHex("#18b750");

			App.IndustryOption = IndustryOptions.Civil;

			App.Current.Resources["selectableButtonStyle"] = App.Current.Resources["civilSelectableButtonStyle"];

			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["civilTrafficAccordionStyle"];
			App.Current.Resources["calendarAccordionStyle"] = App.Current.Resources["civilCalendarAccordionStyle"];
			App.Current.Resources["locationAccordionStyle"] = App.Current.Resources["civilLocationAccordionStyle"];
			App.Current.Resources["weatherAccordionStyle"] = App.Current.Resources["civilWeatherAccordionStyle"];

			App.Current.Resources["primaryButtonStyle"] = App.Current.Resources["buttonGreenPrimary"];
			App.Current.Resources["secondaryButtonStyle"] = App.Current.Resources["buttonGreenSecondary"];

            await Navigation.PushAsync(new AccordionPage());
        }

        private async void TapMiningButton(object sender, EventArgs e)
		{
			((NavigationPage)App.Current.MainPage).BarBackgroundColor = Color.FromHex("#079ece");
			
			App.IndustryOption = IndustryOptions.Mining;

			App.Current.Resources["selectableButtonStyle"] = App.Current.Resources["miningSelectableButtonStyle"];

			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["miningTrafficAccordionStyle"];
			App.Current.Resources["trafficAccordionStyle"] = App.Current.Resources["miningTrafficAccordionStyle"];
			App.Current.Resources["calendarAccordionStyle"] = App.Current.Resources["miningCalendarAccordionStyle"];
			App.Current.Resources["locationAccordionStyle"] = App.Current.Resources["miningLocationAccordionStyle"];
			App.Current.Resources["weatherAccordionStyle"] = App.Current.Resources["miningWeatherAccordionStyle"];

			App.Current.Resources["primaryButtonStyle"] = App.Current.Resources["buttonBluePrimary"];
			App.Current.Resources["secondaryButtonStyle"] = App.Current.Resources["buttonBlueSecondary"];

            await Navigation.PushAsync(new AccordionPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

