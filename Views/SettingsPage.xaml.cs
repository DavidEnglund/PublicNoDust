using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            MainPicker.Items.Add("Phone");
            MainPicker.Items.Add("Email");

            MainPicker.SelectedIndex = 0;

        }
    }
}
