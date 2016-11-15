using System;
using Xamarin.Forms;

namespace Dustbuster
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
	        this.BindingContext = new SettingsViewModel();

            InitializeComponent();
            
            ContactMethodPicker.SelectedIndexChanged += ContactMethodPicker_SelectedIndexChanged;
           
            if (ContactMethodPicker.SelectedIndex == 0)     //select mobile number (0) or email (1)
            {
                // enter mobile number with place holder to show number format 
                etContactInfo.Keyboard = Keyboard.Numeric;
                etContactInfo.Placeholder = "E.g. 04 123 456 78";
            }
            else
            {
                // enter email with place holder to show format of how it should be written
                etContactInfo.Keyboard = Keyboard.Email;
                etContactInfo.Placeholder = "E.g. example@email.com";
            }
        }

        private void ContactMethodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactMethodPicker.SelectedIndex == 1)
            {
                // enter email with place holder to show format
                etContactInfo.Keyboard = Keyboard.Email;
                etContactInfo.Placeholder = "E.g. example@email.com";
            }
            else
            {
                // enter mobile number with place holder to show format of how it should be written
                etContactInfo.Keyboard = Keyboard.Numeric;
                etContactInfo.Placeholder = "E.g. 04 123 456 78";
            }

            etContactInfo.Text = "";
        }

        private void onlineHelpLink_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Makes the items unselectable (so they don't remain highlighted)
            if (e.SelectedItem == null)
            {
                return;     //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;

            Link link = (Link)((ListView)sender).SelectedItem;
            Device.OpenUri(new Uri(link.URL));
        }
    }
}
