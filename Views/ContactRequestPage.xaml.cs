﻿using Dustbuster.Views;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Dustbuster;

namespace Dustbuster
{
    public partial class ContactRequestPage : ContentPage
    {
        /// <summary>
        /// Handles the input from the Contact Request Page, validates the entry fields and other view logic
        /// </summary>
                
        public ContactRequestPage()
        {
            string industryColor = "";

            this.BindingContext = new ContactRequestViewModel(this);
            InitializeComponent();

            // get industry color
            if (App.IndustryOption == IndustryOptions.Civil)
            {
                industryColor = "#18b750";
            }
            else if (App.IndustryOption == IndustryOptions.Mining)
            {
                industryColor = "#079ece";
            }
            // set industry color
            btnSubmit.BackgroundColor = Color.FromHex(industryColor);

            // sync settings username to contact request page user name 
            if (etName.Text != null)
            {
                etName.Text = etName.Text.Trim();
            }
            else
            {
                etName.Text = Settings.CustomerName.Trim();
            }

            // sync settings contact method to contact request page contact method
            if (Settings.ContactMethod == 0)    //Phone
            {
                etContact.Placeholder = "04 123 456 78";
                etContact.Keyboard = Keyboard.Telephone;
                btnPhone.Text = "Phone";
            }
            else    //Email
            {
                etContact.Placeholder = "johndoe@mail.com";
                etContact.Keyboard = Keyboard.Email;
                btnPhone.Text = "Email";
            }

            // sync settings number to contact request page user name
            if (etContact.Text != null)
            {
                etContact.Text = etContact.Text.Trim();
            }
            else
            {
                etContact.Text = Settings.ContactInfo.Trim();
            }
        }

        
        public DatePicker DatePicker
        {
            get { return this.dpDate; }
        }
        

        string contactType = "Email"; // either Phone or Email        
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
            string selectTime = await DisplayActionSheet("Select Time", "", null, "Morning", "Midday", "Afternoon", "Evening");
            // if the user selects out of the popup, go with the last selected

            string pickTime = null;
            if (selectTime != null)
                pickTime = selectTime;

            
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
            if (pickTime != null)
                button.Text = pickTime;
        }

        private async void OnSubmitClicked(object sender, EventArgs args)
        {

            Boolean WebContactResult;
            String industryType;

            Button button = (Button)sender;

            if(App.IndustryOption == IndustryOptions.Civil)
            {
                industryType = "Sunhawk";
            }
            else
            {
                industryType = "Rainstorm";
            }

            // validate the text entry fields
            if (ValidateFields())
            {
                DateTime datePick = DateTime.Now;                       
                //ContactRequestInfo requestInfo = new ContactRequestInfo(etName.Text, etContact.Text, datePick); 


                /*
                if(COntactRequestInfo sends corretly)
                {
                    await Navigation.PushAsync(new CallEmailResultsPage(3));
                }
                else
                {
                    await Navigation.PushAsync(new CallEmailResultsPage(4));
                }
                */

                //Invoking WebService and sending contact data from app to linux server R.L
               
                bool IsEmail = Regex.IsMatch(etContact.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (IsEmail)
                {
                    SendContactsClient contactSend = new SendContactsClient();
                    WebContactResult = await contactSend.SendContactClient(etContact.Text, datePick, etName.Text, UserInfoPHP.Instance.jobLocation, contactType, industryType);
                    

                    if (WebContactResult)
                    {
                        await DisplayAlert("Contact Submitted", "Your details have been forwarded.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Contact Failed", "Please Re-enter details", "Ok");
                    }
                   
                }
                else
                {
                    SendContactsClient contactSend = new SendContactsClient();
                    WebContactResult = await contactSend.SendContactClient(etContact.Text, datePick, etName.Text, UserInfoPHP.Instance.jobLocation, contactType, industryType);


                    if (WebContactResult)
                    {
                        await DisplayAlert("Contact Submitted", "Your details have been forwarded.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Contact Failed", "Please Re-enter details", "Ok");
                    }
                }

                // INSERT code to go back to main page
                await Navigation.PopToRootAsync();
            }
            
        }

        // calls the methods used to validate the input fields
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
                    DisplayAlert("Whoops!", "Incorrect input in Name field.", "Ok");
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
