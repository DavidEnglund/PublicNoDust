using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Dustbuster
{
    public partial class ContactRequestPage : ContentPage
    {
        /// <summary>
        /// Handles the input from the Contact Request Page, validates the entry fields and other view logic
        /// </summary>
                
        public ContactRequestPage()
        {
            this.BindingContext = new ContactRequestViewModel(this);
            InitializeComponent();
        }

        public DatePicker DatePicker
        {
            get { return this.dpDate; }
        }

        string timeSelected;

        // Pulls up the time select menu; morning, afternoon, evening
        async void OnTimeClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // use display action sheet to bring the popup up
            timeSelected = await DisplayActionSheet("Select Time", "Cancel", null, "Morning", "Afternoon", "Evening");
            button.Text = timeSelected;
        }

        async void OnSubmitClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // validate the text entry fields
            if (ValidateFields())
            {
                var reminder = await DisplayAlert("So far, so good!", "Would you like to set a reminder in your phone's calendar?", "Yes", "No");
                if (reminder)
                {                               
                    DependencyService.Get<IReminderervice>().AddReminder("Call Sunhawk", "ph: (08) 9459 2785", DatePicker.Date, timeSelected);
                    Debug.WriteLine("Call Sunhawk", "ph: (08) 9459 2785", DatePicker.Date, timeSelected);
                }                
                    
               
                // create data info object - Send this to wherever it needs to go..
                Debug.WriteLine("LOG: Creating Contact Request Info Object NAME:{0} PH:{1} DATE:{2} TIME:{3}", etName.Text, etPhone.Text, dpDate.Date, btnTime.Text);
                ContactRequestInfo requestInfo = new ContactRequestInfo(etName.Text, etPhone.Text, dpDate.Date, btnTime.Text);
            }            
        }
        // calls the methods used to validate the input fields
        private bool ValidateFields()
        {
            return (ValidName() && ValidPhone()) ? true : false;
        }
        // check if the input name is valid
        private bool ValidName()
        {
            if (etName.Text != null)
            {
                // stand back, I'm going to try regex!
                if (Regex.IsMatch(etName.Text, @"^[a-z A-Z]+$"))
                {
                    etName.Text = etName.Text.Trim();
                    return true;
                }
                else
                {
                    DisplayAlert("Whoops! Regex", "Incorrect input in Name field.", "Ok");
                    return false;
                }
            } else {
                DisplayAlert("Whoops!", "You forgot to input your name.", "Ok");
                return false;
            }
        }
        // check if the phone number is valid
        private bool ValidPhone()
        {
            if (etPhone.Text != null)
            {
                etPhone.Text = etPhone.Text.Trim();
                return true;
            } else {
                DisplayAlert("Whoops!", "You forgot to input your phone number", "Ok");
                return false;
            }
        }
    }
}
