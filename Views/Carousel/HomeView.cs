using Xamarin.Forms;

namespace Dustbuster
{
    class HomeView : ContentView
    {
        // not really required but is a data template to show model elements

        public HomeView()
        {
            BackgroundColor = Color.White;

            Label labelTitle = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };

            labelTitle.SetBinding(Label.TextProperty, "Title");
            this.SetBinding(ContentView.BackgroundColorProperty, "Background");

            Label labelBlurb = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            labelBlurb.SetBinding(Label.TextProperty, "Blurb");

            Image productImage = new Image
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,     //TODO: This is not filling vertically
                Aspect = Aspect.AspectFill
            };
            productImage.SetBinding(Image.SourceProperty, "ImageSource");


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    //labelTitle,
                    //labelBlurb
                    productImage
                }
            };
        }
    }
}