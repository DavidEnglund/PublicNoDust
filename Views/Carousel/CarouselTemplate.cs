
using Xamarin.Forms;

namespace Dustbuster
{
    /// <summary>
    ///  This class will be converted to the XAML for the carousel
    /// </summary>
    class CarouselTemplate : ContentView
    {
        // not really required but is a data template to show model elements

        public CarouselTemplate()
        {
            BackgroundColor = Color.White;



            #region Page Elements
            Label lblTitle = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("#ffffff"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                
                Text = "The quick brown fox jumps over the lazy dog",
                
            };
                       
            Label lblDescription = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Text = "The quick Ken jumps over the lazy mac",
            };

            Label lblQuantity = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Text = "The quick Ken jumps over the lazy mac",
            };

            Label lblApplication = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Text = "The quick Ken jumps over the lazy mac",
            };

            Label lblRate = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Text = "The quick Ken jumps over the lazy mac",
            };

            Image productImage = new Image
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,     //TODO: This is not filling vertically
                Aspect = Aspect.AspectFill
            };

            #endregion


            // Add padding to the layout between title and rest of page
            StackLayout stackTitleWrap = new StackLayout
            {
                Padding = new Thickness(0, 10),
                BackgroundColor = Color.FromHex("#18b750"),
            };

            

            StackLayout stackLabelWrap = new StackLayout
            {
                Padding = new Thickness(20),
                BackgroundColor = Color.FromHex("#ffffff"),
                Spacing = 10,
            };

            #region Bindings
            lblTitle.SetBinding(Label.TextProperty, "ProductName");
            lblDescription.SetBinding(Label.TextProperty, "ProductDesc");
            lblQuantity.SetBinding(Label.TextProperty, "ApplicationsRequired");
            lblApplication.SetBinding(Label.TextProperty, "ProductName");
            lblRate.SetBinding(Label.TextProperty, "ApplicationsRequired");
            productImage.SetBinding(Image.SourceProperty, "ImageSource");
            #endregion

            stackTitleWrap.Children.Add(lblTitle);

            stackLabelWrap.Children.Add(lblDescription);
            stackLabelWrap.Children.Add(lblQuantity);
            stackLabelWrap.Children.Add(lblRate);
            stackLabelWrap.Children.Add(lblApplication);
                        
           
            
            Content = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    
                    stackTitleWrap,
                    productImage,
                    stackLabelWrap,
                    
                                     
                }
            };
        }
    }
}