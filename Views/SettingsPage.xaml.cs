﻿using System;
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

           
        }
    }
}
