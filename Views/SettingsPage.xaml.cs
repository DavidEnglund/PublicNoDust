using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            
            if (ContactMethodPicker.SelectedIndex == 0)     //select mobile number or email
            {
                // enter mobile number with place holder to show number format 
                ContactInfoEntry.Keyboard = Keyboard.Numeric; 
                ContactInfoEntry.Placeholder = "E.g. 04 123 456 78";
            }
            else
            {
                // enter email with place holder to show format of how it should be written
                ContactInfoEntry.Keyboard = Keyboard.Email;
                ContactInfoEntry.Placeholder = "E.g. example@email.com";
            }
        }

        private void ContactMethodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactMethodPicker.SelectedIndex == 1)
            {
                // enter email with place holder to show format
                ContactInfoEntry.Keyboard = Keyboard.Email;
                ContactInfoEntry.Placeholder = "E.g. example@email.com";
            }
            else
            {
                // enter mobile number with place holder to show format of how it should be written
                ContactInfoEntry.Keyboard = Keyboard.Numeric;
                ContactInfoEntry.Placeholder = "E.g. 04 123 456 78";
            }

           
        }
    }
}
