using Dustbuster.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster
{
    public partial class ProductPage : ContentPage
    {
        string PageColor = "";

        public ProductPage(ProductViewModel viewModel)
        {
			BindingContext = viewModel;
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            InitializeCarouselView();
        }

        private void InitializeCarouselView()
        {
            //Here we set the color of our dots, and buttons depending on if the user chooses civil or mining
            if (App.IndustryOption == IndustryOptions.Civil)
            {
                PageColor = "#18b750";
            }
            else if (App.IndustryOption == IndustryOptions.Mining)
            {
                PageColor = "#079ece";
            }
            btnCallNow.BackgroundColor = Color.FromHex(PageColor);
            btnRequestContact.BorderColor = Color.FromHex(PageColor);
            btnRequestContact.TextColor = Color.FromHex(PageColor);

            CarouselLayout pagesCarousel = CreatePagesCarousel();
            View dots = new StackLayout
            {
                Children = { CreatePagerIndicators() }
            };

            // Sets the position padding and dimensions of the Carousel pages (Yay!)
            // Position X, Position Y; Dimension X, Dimension Y
            // NOTE: Only really need to change Dimension Y to comply with the design.
            CarouselView.Children.Add(pagesCarousel,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return 0; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height; })
                        );

            CarouselView.Children.Add(dots,
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => { return parent.Height - 9.8; }),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.Constant(10)
            );
        }

        //This method creates the carousel
        private CarouselLayout CreatePagesCarousel()
        {
            var carousel = new CarouselLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

                ItemTemplate = new DataTemplate(typeof(CarouselTemplate))
            };
            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Products");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "SelectedProduct", BindingMode.TwoWay);

            return carousel;
        }

        //This method creates the dots representing every page in the carousel
        private View CreatePagerIndicators()
        {
            PagerIndicatorDots pagerIndicator = new PagerIndicatorDots() { DotSize = 15, DotColor = Color.FromHex(PageColor) };
            pagerIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "Products");
            pagerIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentProduct");

            return pagerIndicator;
        }       

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private async void OnCallNow_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IDialer>().Dial("0425448555");
        }

        private async void OnRequestContact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactRequestPage());
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

            // interum testing
            foreach (string[] testingMatrixResult in productStrings)
            {
                    Debug.WriteLine(testingMatrixResult[0] + " ----- " + testingMatrixResult[1]);
            }
                // and finally we need to put this data in a carousel data source that I dont have yet
                /*
                List<HomeViewModel> CarouselSource = new List<HomeViewModel>();
               foreach (string[] fromDataBase in productStrings)
               {
                    //TODO: add the rest of the carousle code when the carousel is in this project
                    
               }


                /**/
                });


        }
    }
}