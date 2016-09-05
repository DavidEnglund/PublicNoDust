using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dustbuster
{
    public partial class ContactRequestPage : ContentPage
    {
        public ContactRequestPage()
        {
            this.BindingContext = new ContactRequestViewModel(this);
            InitializeComponent();
        }

        public DatePicker DatePicker
        {
            get { return this.dpDate; }
        }
        
        // Pulls up the time select menu; morning, afternoon, evening
        async void OnTimeClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // use display action sheet to bring the popup up
            var timeSelected = await DisplayActionSheet("Select Time", "Cancel", null, "Morning", "Afternoon", "Evening");
            button.Text = timeSelected;
        }

        async void OnSubmitClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // validate the text entry fields
            if (ValidateFields())
            {
                Debug.WriteLine("Log: Entries are valid.");
                var reminder = await DisplayAlert("So far, so good!", "Would you like to set a reminder in your phone's calendar?", "Yes", "No");
                if (reminder)
                {
                    Debug.WriteLine("Setting a reminder at date: ");
                    // TODO: HEY DAVID, ADD DEPENDENCY SERIVCE FOR REMIDNER HERE
                    // REMINDER REMINDERSERVICE = NEW REMINDER();
                    // REMINDERSERVICE.ADDREMINDER(STRING,DATETIME);
                }
                else
                {
                    Debug.WriteLine("Skipping setting a reminder");
                }

                // create data info object
                Debug.WriteLine("LOG: Creating Contact Request Info Object NAME:{0} PH:{1} DATE:{2} TIME:{3}", etName.Text, etPhone.Text, dpDate.Date, btnTime.Text);
                ContactRequestInfo requestInfo = new ContactRequestInfo(etName.Text, etPhone.Text, dpDate.Date, btnTime.Text);
            }
            else
            {
                Debug.WriteLine("LOG: Entries are IN-valid");
            }
        }

        private bool ValidateFields()
        {
            Debug.WriteLine("LOG: Beginning validation process");
            return (ValidName() && ValidPhone()) ? true : false;
        }
        private bool ValidName()
        {
            if (etName.Text != null)
            {
                // stand back, I'm going to try regex!
                Debug.WriteLine("LOG: Validating name");
                if (Regex.IsMatch(etName.Text, @"^[a-zA-Z]+$"))
                {
                    Debug.WriteLine("LOG: Name is valid");
                    return true;
                }
                else
                {
                    DisplayAlert("Hey, Listen!", "Incorrect input in Name field.", "Ok");
                    Debug.WriteLine("LOG: Incorrect input in name field");
                    return false;
                }
            } else {
                DisplayAlert("Hey, Listen!", "You forgot to input your name.", "Ok");
                return false;
            }
        }
        private bool ValidPhone()
        {
            if (etPhone.Text != null)
            {
                etPhone.Text = etPhone.Text.Trim();
                Debug.WriteLine("LOG: Validating phone number");
                return true;
            } else {
                DisplayAlert("Hey, Listen!", "You forgot to input your phone number", "Ok");
                return false;
            }
        }
    }
}
