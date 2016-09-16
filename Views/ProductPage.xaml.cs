﻿using System;
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
        public static SwitcherPageViewModel viewModel;

        public ProductPage()
        {
            Debug.WriteLine("LOG 001 ~ Product Page Constructor");
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            InitializeCarouselView();
        }

        private void InitializeCarouselView()
        {
            viewModel = new SwitcherPageViewModel();
            BindingContext = viewModel;

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
                        Constraint.RelativeToParent((parent) => { return parent.Height - 8; }),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.Constant(10)
            );
        }

        //This method creates the carousel
        private CarouselLayout CreatePagesCarousel()
        {
            Debug.WriteLine("LOG 004 ~ CreatePagesCarousel method");
            
            var carousel = new CarouselLayout ()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                
                ItemTemplate = new DataTemplate(typeof(CarouselTemplate))
            };
            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Products");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentProduct", BindingMode.TwoWay);

            return carousel;
        }

        private View CreatePagerIndicators()
        {
            Debug.WriteLine("LOG 007 ~ CreatePagerIndicators called");
            PagerIndicatorDots pagerIndicator = new PagerIndicatorDots() { DotSize = 15, DotColor = Color.Black };
            pagerIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "Products");
            pagerIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentProduct");

            return pagerIndicator;
        }
       
        private async void TapProductButton(object sender, EventArgs e)
        {
            //Go back to the product information
        }

        private async void TapCalandarButton(object sender, EventArgs e)
        {
            //GO to what ever page creates a reminder on the calanda
        }

        private async void Button_CallNow(object sender, EventArgs e)
        {
            //GO to what ever page allows the user to make a phone call now
            // DIALER
        }

        private async void Button_RequestCall(object sender, EventArgs e)
        {
            // YO MIKEY! MAKE THIS BUTTON GO TO YOUR FAB CONTACT REQUEST PAGE
            // remove comment once merged
            // await Navigation.PushAsync(new ContactRequestPage());

            
        }

      
    }
}
