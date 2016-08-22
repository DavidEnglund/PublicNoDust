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

        private async void Button_ToAccordion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccordionPage());
        }
    }
}

