using Xamarin.Forms;

namespace Dustbuster
{
    class CarouselTemplate : ContentView
    {
        Label lblTitle, lblDescription, lblQuantity, lblApplication, lblRate;
        StackLayout stackTitleWrap, stackLabelWrap, parent;
        Image productImage;

        public CarouselTemplate()
        {
            CreateElements();
            SetBindings();

            BackgroundColor = Color.White;
            Content = parent;
        }


        private void CreateElements()
        {
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
            

            stackTitleWrap = new StackLayout
            {
                Padding = new Thickness(0, 10),     // Add padding to the layout between title and rest of page
                BackgroundColor = Color.FromHex("#18b750"),
            };

            stackLabelWrap = new StackLayout
            {
                Padding = new Thickness(20),
                BackgroundColor = Color.FromHex("#ffffff"),
                Spacing = 10,
            };

            parent = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    stackTitleWrap,
                    productImage,
                    stackLabelWrap
                }
            };


            stackTitleWrap.Children.Add(lblTitle);

            stackLabelWrap.Children.Add(lblDescription);
            stackLabelWrap.Children.Add(lblQuantity);
            stackLabelWrap.Children.Add(lblRate);
            stackLabelWrap.Children.Add(lblApplication);

            parent.Children.Add(stackTitleWrap);
            parent.Children.Add(productImage);
            parent.Children.Add(stackLabelWrap);
        }

        private void SetBindings()
        {
            lblTitle.SetBinding(Label.TextProperty, "ProductName");
            lblDescription.SetBinding(Label.TextProperty, "ProductDesc");
            lblQuantity.SetBinding(Label.TextProperty, "ApplicationsRequired");
            lblApplication.SetBinding(Label.TextProperty, "ProductName");
            lblRate.SetBinding(Label.TextProperty, "ApplicationsRequired");
            productImage.SetBinding(Image.SourceProperty, "ImageSource");
        }
    }
}