using Dustbuster.Views.Carousel;
using System;
using Xamarin.Forms;

namespace Dustbuster
{
    class CarouselTemplate : ContentView
    {
        Label lblTitle, lblDescription, lblQuantity, lblApplication, lblRate;
        StackLayout stackTitleWrap, parent;
        ScrollView scrollingLabelWrap;
        Image productImage;
        string PageColor = "";

        public CarouselTemplate()
        {
            CreateElements();
            SetBindings();

            BackgroundColor = Color.White;
            Content = parent;
        }


        private void CreateElements()
        {
            //Here we set the logo and the color of the carousel page depedning on if the user chooses civil or mining
            if (App.IndustryOption == IndustryOptions.Civil)
            {
                PageColor = "#18b750";
            }
            else if (App.IndustryOption == IndustryOptions.Mining)
            {
                PageColor = "#079ece";
            }

            lblTitle = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#ffffff"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Text = "The quick brown fox jumps over the lazy dog",
            };

            lblDescription = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#5a5d5e"),
                Text = "The quick Ken jumps over the lazy mac",
            };

            lblQuantity = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#5a5d5e"),
                Text = "The quick Ken jumps over the lazy mac",
            };

            lblApplication = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#5a5d5e"),
                Text = "The quick Ken jumps over the lazy mac",
            };

            lblRate = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#5a5d5e"),
                Text = "The quick Ken jumps over the lazy mac",
            };
            
            productImage = new Image
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFill
            };

            //Assign tap gesture to ProductImage
            AddImageTap();

            stackTitleWrap = new StackLayout
            {
                Padding = new Thickness(0, 10),     // Add padding to the layout between title and rest of page
                Spacing = 0,
                BackgroundColor = Color.FromHex(PageColor),
            };

            //Everything inside of here will be able to scoll vertically
            scrollingLabelWrap = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,

                //This is were the main labels are placed into
                Content = new StackLayout
                {
                    Padding = new Thickness(20),
                    BackgroundColor = Color.FromHex("#f2f2f2"),
                    Spacing = 10,
                    Children =
                    {
                        lblDescription,
                        lblQuantity,
                        lblRate,
                        lblApplication
                    }
                }
        };

            parent = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    stackTitleWrap,
                    productImage,
                    scrollingLabelWrap
                }
            };

            stackTitleWrap.Children.Add(lblTitle);

            parent.Children.Add(stackTitleWrap);
            parent.Children.Add(productImage);
            parent.Children.Add(scrollingLabelWrap);
        }

        /// <summary>
        /// This method adds a tap gesture to the product image
        /// </summary>
        private void AddImageTap()
        {
            //Create a new tap gesture and assign it to the image
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                ImageTapEvent(s, e);
            };
            productImage.GestureRecognizers.Add(tapGestureRecognizer);
        }

        bool disable = false;

        //This method handles the tap event which opens the ImageViewer
        private async void ImageTapEvent(object sender, EventArgs e)
        {
            if (this.disable)
                return;

            this.disable = true;
            await Navigation.PushModalAsync(new ImageViewer(productImage));
            this.disable = false;
        }

        private void SetBindings()
        {
            lblTitle.SetBinding(Label.TextProperty, "ProductName");
            lblDescription.SetBinding(Label.TextProperty, "ProductDesc");
            lblQuantity.SetBinding(Label.TextProperty, new Binding("ProductQuantity", stringFormat: "Estimated amount per application: {0}L"));
            lblApplication.SetBinding(Label.TextProperty, new Binding("ApplicationsRequired", stringFormat: "Applications required: {0}"));
            lblRate.SetBinding(Label.TextProperty, new Binding("ApplicationRate", stringFormat: "Application rate: {0}mL/m\xB2"));
            productImage.SetBinding(Image.SourceProperty, "ImageSource");
        }
    }
}