using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dustbuster.Views.Carousel
{
    class ImageViewer : ContentPage
    {
        private Image _image;

        public ImageViewer(Image image)
        {
            this._image = image;
            Content = SetupView();
        }

        private RelativeLayout SetupView()
        {
            //Initial layout to hold image
            RelativeLayout layout = new RelativeLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            
            //Add image to initial layout and set constraints
            layout.Children.Add(new Image
            {
                Source = _image.Source,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            },  Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; }));

            //Create a stack layout to hold the close icon
            StackLayout stackLayout = new StackLayout
            {
                Padding = new Thickness(5, 5, 5, 5)
            };

            //Create and add the close icon to the stack layout
            Image crossImage = CreateCrossImage();
            stackLayout.Children.Add(crossImage);

            //Add the stack layout to the relative layout and add constraints so the icon can be aligned horizontaly
            layout.Children.Add(stackLayout, 
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }));

            return layout;
        }

        /// <summary>
        /// This method creates the cross icon and adds the tap gesture to the icon.
        /// </summary>
        /// <returns>Cross Icon (Image)</returns>
        private Image CreateCrossImage()
        {
            //Create new image, align it to the end and add the cross_icon source
            Image crossImage = new Image
            {
                HorizontalOptions = LayoutOptions.End,
                Source = ImageSource.FromFile("cross_icon.png")
            };

            //Create a new tap gesture, add the tap event and assign the tap gesture to the cross icon image
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                CrossImageTapEvent(s, e);
            };
            crossImage.GestureRecognizers.Add(tapGestureRecognizer);

            return crossImage;
        }

        //This method is the event handler for when the cross_icon is tapped, it will navigate to previous page
        private void CrossImageTapEvent(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
