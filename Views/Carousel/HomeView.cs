using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dustbuster
{
    //data template to show data
    class HomeView : ContentView
    {
        // not really required but is a data template to show model elements

        public HomeView()
        {
            BackgroundColor = Color.White;

            var label0 = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };

            label0.SetBinding(Label.TextProperty, "Title");
            this.SetBinding(ContentView.BackgroundColorProperty, "Background");

            var label1 = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };

            label1.SetBinding(Label.TextProperty, "Blurb");


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    label0,
                    label1
                }
            };
        }
    }
}