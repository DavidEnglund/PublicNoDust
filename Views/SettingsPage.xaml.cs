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
        }

        private void ContactMethodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactMethodPicker.SelectedIndex == 1)
            {
                ContactInfoEntry.Keyboard = Keyboard.Email;
            }
            else
            {
                ContactInfoEntry.Keyboard = Keyboard.Numeric;
            }
        }
    }
}
