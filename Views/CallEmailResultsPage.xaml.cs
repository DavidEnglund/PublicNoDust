using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster.Views
{
    public partial class CallEmailResultsPage : ContentPage
    {
        public CallEmailResultsPage(int result)
        {
            InitializeComponent();
            SetState(result);
        }

        public void SetState(int state)
        { //This is how we set what styling should be set when the page is called
            string sucessImage = "success_tick.png", failImage = "fail_cross.png", color = "", stateText = "", mainText = "", btnOne = "", btnTwo = "";
            EventHandler btnOneEvent, btnTwoEvent;

            switch (state)
            {
                case 1:
                    //Call Success
                    color = "#18b750";
                    stateText = "Call Successful!";
                    mainText = "Your call was successful, feel free to go back to the product page or to the home page, we hope you have a lovely day.";
                    btnOne = "Product Page";
                    btnTwo = "Home Page";
                    btnOneEvent = Button_ProductPage;
                    btnTwoEvent = Button_HomePage;
                    SetInformation(color, stateText, mainText, btnOne, btnOneEvent, btnTwo, btnTwoEvent, sucessImage);
                    break;

                case 2:
                    //Call Fail
                    color = "#df4347";
                    stateText = "Call Failed!";
                    mainText = "Your call failed, however you can attempt to send a contact request or add a reminder to your phones calendar.";
                    btnOne = "Create Reminder";
                    btnTwo = "Request Contact";
                    btnOneEvent = Button_CreateReminder;
                    btnTwoEvent = Button_RequestContact;
                    SetInformation(color, stateText, mainText, btnOne, btnOneEvent, btnTwo, btnTwoEvent, failImage);
                    break;

                case 3:
                    //Contact Request Form Success
                    color = "#18b750";
                    stateText = "Your information has been successfully sent!";
                    mainText = "Your Email was successfully sent, feel free to go back to the product page or to the home page, we hope you have a lovely day.";
                    btnOne = "Product Page";
                    btnTwo = "Home Page";
                    btnOneEvent = Button_ProductPage;
                    btnTwoEvent = Button_HomePage;
                    SetInformation(color, stateText, mainText, btnOne, btnOneEvent, btnTwo, btnTwoEvent, sucessImage);
                    break;

                case 4:
                    //Contact Request Form Fail
                    color = "#df4347";
                    stateText = "Your information has failed to send!";
                    mainText = "Your Email has failed to send, however you can return to the product page or add a reminder to your phones calendar.";
                    btnOne = "Product Page";
                    btnTwo = "Request Contact";
                    btnOneEvent = Button_ProductPage;
                    btnTwoEvent = Button_RequestContact;
                    SetInformation(color, stateText, mainText, btnOne, btnOneEvent, btnTwo, btnTwoEvent, failImage);
                    break;
            }
        }

        public void SetInformation(string color, string stateText, string mainText, string btnOneText, EventHandler btnOneClick, string btnTwoText, EventHandler btnTwoClick, string image)
        { //This sets the page to its new styling and the buttons to the correct methods
            ColorBackgroundOne.BackgroundColor = Color.FromHex(color);
            ColorBackgroundTwo.BackgroundColor = Color.FromHex(color);
            ColorBackgroundThree.BackgroundColor = Color.FromHex(color);
            StateLabel.Text = stateText;
            MainTextLabel.Text = mainText;
            ButtonOne.Text = btnOneText;
            ButtonOne.Clicked += btnOneClick;
            ButtonOne.TextColor = Color.FromHex(color);
            ButtonTwo.Text = btnTwoText;
            ButtonTwo.Clicked += btnTwoClick;
            ButtonTwo.TextColor = Color.FromHex(color);
            MainImage.Source = image;
        }

        private async void TapCloseButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccordionPage());
        }

        private async void Button_CreateReminder(object sender, EventArgs e)
        {
            //Take the user to create a reminder on the calander
        }

        private async void Button_RequestContact(object sender, EventArgs e)
        {
            //Take the user to send a contact request
        }

        private async void Button_ProductPage(object sender, EventArgs e)
        {
            //Send the user back to the product page
        }

        private async void Button_HomePage(object sender, EventArgs e)
        {
            //Send the user to the home page
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}