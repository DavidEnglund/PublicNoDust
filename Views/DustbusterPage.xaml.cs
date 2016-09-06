using Dustbuster;
using Dustbuster.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class DustbusterPage : ContentPage
	{
        private bool panelShowing = false;
        private List<ProductResult> products;

        public DustbusterPage()
		{
			InitializeComponent();

            populateListView();
        }

        private void populateListView()
        {
            products = new List<ProductResult>();
            products.Add(new ProductResult() { ProductName = "Gluon", TimeStamp = DateTime.Now, Amount = "85000 L" });
            products.Add(new ProductResult() { ProductName = "DustLig", TimeStamp = DateTime.Now, Amount = "120 L" });
            products.Add(new ProductResult() { ProductName = "Gluon26", TimeStamp = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", TimeStamp = DateTime.Now, Amount = "180 L" });
            products.Add(new ProductResult() { ProductName = "DustJel", TimeStamp = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", TimeStamp = DateTime.Now, Amount = "160 L" });
            products.Add(new ProductResult() { ProductName = "DustJel", TimeStamp = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", TimeStamp = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "DustLig", TimeStamp = DateTime.Now, Amount = "177 L" });

            listViewHistory.ItemsSource = products;
        }

        private async void AnimatePanel()
        {
            // swap the state
            this.PanelShowing = !PanelShowing;

            if (this.PanelShowing)
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
            //TODO  Make it display the corresponding result
            throw new NotImplementedException();
        }

        private async void btnSettings_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void btnAbout_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new AboutPage());
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


        private bool PanelShowing
        {
            get { return panelShowing; }
            set { panelShowing = value; }
        }
    }
}

