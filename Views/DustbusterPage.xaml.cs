using Dustbuster;
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
            //await Navigation.PushAsync(new AccordionPage());
            await Navigation.PushAsync(new ContactRequestPage());
        }

        private async void TapMiningButton(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AccordionPage());
            await Navigation.PushAsync(new ContactRequestPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

