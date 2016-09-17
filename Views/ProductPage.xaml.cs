using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster.Views
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        private async void TapProductButton(object sender, EventArgs e)
        {
            //Go back to the product information
        }

        private async void TapCalandarButton(object sender, EventArgs e)
        {
            //GO to what ever page creates a reminder on the calanda
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_CallNow(object sender, EventArgs e)
        {
            //GO to what ever page allows the user to make a phone call now
        }

        private async void Button_RequestCall(object sender, EventArgs e)
        {
            //GO to what ever page allows a call request
        }

        // getting the data from the database and formatting it and creating  carousel data object - David
       
        private async void getProductData()
        {
            // set the carousel's data source to an object
            DbConnectionManager connection = new DbConnectionManager();
            Job productSelections = new Job();
            productSelections.AreaTypeID = (int)App.TrafficOption;
            productSelections.WillRain = Equals(App.WeatherOption,WeatherOptions.RainExpected);
            productSelections.DurationMaxDays = (int)App.DurationOption;
            // there is no company selection in the database for product matrix yet but there is one in the job object that is used to call for the matrix
            productSelections.JobType = (int)App.IndustryOption;
            // not sure how to get the area so a place holder of 1000 for now
            productSelections.Area = 1000;
            // other non selection data for the job object
            productSelections.CreationDate = DateTime.Now;
            // now an await call to the db to get the data
            IEnumerable<ProductMatrix> productSelectionList;
            await Task.Run(() =>
            {
                productSelectionList = connection.SelectProducts(productSelections);
                // I dont have the carousel layout here yet so a temp object it is
                List<string[]> productStrings = new List<string[]>();
                foreach (ProductMatrix matrix in productSelectionList)
                {
                    // get the product info from the database, format a string with the description and application info then add  it to a string list with the name
                    ProductDescription productDescription = connection.GetProductInfo(matrix);
                    string formattedDescription = productDescription.ProductDesc + "\r\n you will need " + productDescription.ApplicationsRequired +
                    "\r\n you will reqiure " + productDescription.GetQuantity(productSelections.Area) + " per application";
                    
                    productStrings.Add(new string[] { productDescription.ProductName, formattedDescription });
                }
                // and finally we need to put this data in a carousel data source that I dont have yet
                /*
                List<HomeViewModel> CarouselSource = new List<HomeViewModel>();
               foreach (string[] fromDataBase in productStrings)
               {
                    //TODO: add the rest of the carousle code when the carousel is in this project
                    
               }


                */
                });


        }
    }
}
