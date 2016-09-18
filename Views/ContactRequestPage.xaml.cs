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

        #region Page Buttons
        string contactType; // either Phone or Email        
        double remindTime;
        // Pulls up the contact type selcted menu
        private async void OnContactClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            contactType = await DisplayActionSheet("Select Method of Contact", "", null, "Phone", "Email");
            //change the place holder
            etContact.Text = ""; // clear the text;
            switch (contactType)
            {
                case "Email":
                    etContact.Placeholder = "johndoe@mail.com";
                    etContact.Keyboard = Keyboard.Email;
                    button.Text = "Email";
                    break;
                case "Phone":
                    etContact.Placeholder = "04 123 456 78";
                    etContact.Keyboard = Keyboard.Telephone;
                    button.Text = "Phone";
                    break;
                default:
                    break;
            }
        }

        // Pulls up the time select menu; morning, afternoon, evening
        private async void OnTimeClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // use display action sheet to bring the popup up            
            string pickTime = await DisplayActionSheet("Select Time", "", null, "Morning", "Midday", "Afternoon", "Evening");
            // either Morning(8AM), Midday(12-noon), Afternoon(2pm) or Evening(5pm)
            switch (pickTime)
            {
                case "Evening":
                    remindTime = 16.00;
                    break;
                case "Afternoon":
                    remindTime = 14.00;
                    break;
                case "Midday":
                    remindTime = 12.00;
                    break;
                case "Morning": // defaults to morning
                default:
                    remindTime = 8.00;
                    break;
            }
            button.Text = pickTime;
        }

        private async void OnSubmitClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            // validate the text entry fields
            if (ValidateFields())
            {
                var pickReminder = await DisplayAlert("So far, so good!", "Would you like to set a reminder in your contact's calendar?", "Yes", "No");
                if (pickReminder)
                {
                    // the date and time to be passed into the reminder
                    DateTime remindDate = DatePicker.Date;                   
                    remindDate.AddHours(remindTime);

                    DependencyService.Get<IReminderervice>().AddReminder("Call Sunhawk", "ph: (08) 9459 2785", remindDate);
                    Debug.WriteLine("Call Sunhawk", "ph: (08) 9459 2785", DatePicker.Date, remindDate);
                }
                // create data info object - Send this to wherever it needs to go..                
                ContactRequestInfo requestInfo = new ContactRequestInfo(etName.Text, etContact.Text, dpDate.Date);
            }
        }
        // calls the methods used to validate the input fields
        #endregion

        private bool ValidateFields()
        {
            return (ValidName() && ValidContact()) ? true : false;
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
        // check if the contact number is valid
        private bool ValidContact()
        {
            if (etContact.Text != null)
            {
                etContact.Text = etContact.Text.Trim();
                return true;
            } else {
                DisplayAlert("Whoops!", "You forgot to input your contact info", "Ok");
                return false;
            }
        }
    }
}
