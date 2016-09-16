using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster.Views
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        private async void TapProductButton(object sender, EventArgs e)
        {
            //Go back to the product information
        }

        private async void TapCalandarButton(object sender, EventArgs e)
        {
            //GO to what ever page creates a reminder on the calanda
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_CallNow(object sender, EventArgs e)
        {
            //GO to what ever page allows the user to make a phone call now
        }

        private async void Button_RequestCall(object sender, EventArgs e)
        {
            //GO to what ever page allows a call request
        }
    }
}
