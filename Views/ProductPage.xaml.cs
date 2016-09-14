using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;

namespace Dustbuster.Views
{
    public partial class ProductPage : ContentPage
    {
       

        #region Rebecca's Code
        //RelativeLayout relativeLayout;

        public static SwitcherPageViewModel viewModel;
        
        public ProductPage()
        {
            InitializeComponent();
            Debug.WriteLine("LOG 001 ~ Product Page Constructor");
            
            NavigationPage.SetHasNavigationBar(this, false);

            
            viewModel = new SwitcherPageViewModel();
            BindingContext = viewModel;
            
            //author has bad naming conventions 
            /*relativeLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,


            };*/

            CarouselLayout pagesCarousel = CreatePagesCarousel();
            View dots = CreatePagerIndicatorContainer();
            
            // remove later
            /*CarouselView.Children.Add(CreateTitleLabel(),
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return parent.Y; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height / 8; }));*/



            CarouselView.Children.Add(pagesCarousel,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return 0; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height; })
                        );

            CarouselView.Children.Add(dots,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Height * 0.8; }),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.Constant(10)
            );
            /*
             Constraint.RelativeToView(pagesCarousel,
                    (parent, sibling) => { return sibling.Height - 10; }),
                    */


            //Content = relativeLayout;
        }



        // This method creates a Label
        Label CreateTitleLabel()
        {

            var titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Blue

            };

            titleLabel.SetBinding(Label.TextProperty, "CurrentTitle");


            return titleLabel;
        }

        //This method creates the carousel
        CarouselLayout CreatePagesCarousel()
        {
            Debug.WriteLine("LOG 004 ~ CreatePagesCarousel method");
            var carousel = new CarouselLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(typeof(HomeView))
            };
            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);
            //below the binding only fires while the application is loading. after loading it won't fire
            carousel.SetBinding(CarouselLayout.SelectedTitleProperty, "CurrentTitle", BindingMode.TwoWay);
            //still need to create bindable properties for the blurb

            return carousel;
        }


        private View CreatePagerIndicatorContainer()
        {
            Debug.WriteLine("LOG 006 ~ CreatePagerIndicatorContainer called");
            return new StackLayout
            {
                Children = { CreatePagerIndicators() }
            };
        }

        private View CreatePagerIndicators()
        {
            Debug.WriteLine("LOG 007 ~ CreatePagerIndicators called");
            var pagerIndicator = new PagerIndicatorDots() { DotSize = 15, DotColor = Color.Black };
            pagerIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "Pages");
            pagerIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentPage");
            return pagerIndicator;
        }
        #endregion


        #region Erdal's Buttons
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
            // YO MIKEY! MAKE THIS BUTTON GO TO YOUR FAB CONTACT REQUEST PAGE
            //await Navigation.PushAsync(new ContactRequestPage());

            //GO to what ever page allows a call request
        }
        #endregion
    }
}
