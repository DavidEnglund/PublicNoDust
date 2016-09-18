using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class DustbusterPage : ContentPage
	{
		public DustbusterPage()
		{
			InitializeComponent();
		}

        private async void TapCivilButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccordionPage());
        }

        private async void TapMiningButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccordionPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

