using Dustbuster;
using Dustbuster.Models;
using Dustbuster.Views;
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
            Settings.EnableReadMode = true; //Need it to start of false on start up.
            dustbusterViewModel = new DustbusterViewModel();
            BindingContext = dustbusterViewModel;

            InitializeComponent();
            setTitleImageDescription();
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

        private void btnReadModeButton_Clicked(object sender, EventArgs e)
        {
            setTitleImageDescription();
        }

        //Here we are setting all the elements that have to change depedning on if reading mode is on or off
        public void setTitleImageDescription()
        {
            if (Settings.EnableReadMode)
            {
                lblTitle.TextColor = Color.FromHex("#ffffff");
                lblMainDescription.TextColor = Color.FromHex("#ffffff");
                imgApplicationImage.Source = "app_title.png";
                ReadModeImage.Source = "readingMode_glasses.png";
                HamBurgerImage.Source = "hamburger_icon";
                Settings.EnableReadMode = false;
            }
            else
            {
                lblTitle.TextColor = Color.FromHex("#5a5d5e");
                lblMainDescription.TextColor = Color.FromHex("##5a5d5e");
                imgApplicationImage.Source = "";
                ReadModeImage.Source = "readingMode_img.png";
                HamBurgerImage.Source = "hamburger_icon_black";
                Settings.EnableReadMode = true;
            }
        }

        private void btnSideMenu_Clicked(object sender, EventArgs e)
        {
            AnimatePanel();
        }

        private async void listViewItem_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            AnimatePanel();


            //TODO : Still working on this, it's not 100% functional.

            /*
            //This is only necessary if we want to make the items unselectable (they don't stay highlighted)
            if (e.SelectedItem == null)
            {
                return;     //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;
            */

            /*
            Job jobSelected = (Job)((ListView)sender).SelectedItem;
            var productSelection = App.ProductsDb.SelectProducts(jobSelected);

            ProductViewModel productViewModel = new ProductViewModel(jobSelected.Area);
            foreach (ProductMatrix productMatrix in productSelection)
            {
                ProductDescription productDescription = App.ProductsDb.GetProductInfo(productMatrix);

                productViewModel.Products.Add(productDescription);
                if (productViewModel.Products.Count > 0)
                {
                    productViewModel.SelectedProduct = productViewModel.Products[0];
                }
            }

            //ERROR : At some point (don't know where exactly) this throws a
            //System.ArgumentOutOfRangeException: Specified argument was out of the range of valid values.
            //Parameter name: Empty string
            await Navigation.PushAsync(new ProductPage(productViewModel));
            */
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
            if (App.IndustryOption == IndustryOptions.None)
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
        }

        private async void TapMiningButton(object sender, EventArgs e)
        {
            if (App.IndustryOption == IndustryOptions.None)
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.IndustryOption = IndustryOptions.None;

            NavigationPage.SetHasNavigationBar(this, false);
            dustbusterViewModel = new DustbusterViewModel();
            BindingContext = dustbusterViewModel;
        }
    }
}

