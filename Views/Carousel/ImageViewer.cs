using System;
using Xamarin.Forms;

namespace Dustbuster.Views.Carousel
{
    public class ImageViewer : ContentPage
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
            layout.Children.Add(new PinchToZoomContainer
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = CreateProductImage()
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

        private Image CreateProductImage()
        {
            //Create new image, align it to the end and add the cross_icon source
            Image productImage = new Image
            {
                Source = _image.Source,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            return productImage;
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
                Source = ImageSource.FromFile("cross_icon.png"),
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

        private void ProductImagePinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            
        }
    }

    public class PinchToZoomContainer : ContentView
    {
        private double startScale, currentScale, xOffset, yOffset, startX, startY;

        public PinchToZoomContainer()
        {
            if (Device.OS != TargetPlatform.Android)
            {
                var pinchGesture = new PinchGestureRecognizer();
                pinchGesture.PinchUpdated += OnPinchUpdated;
                GestureRecognizers.Add(pinchGesture);

                var panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += OnPanUpdated;
                GestureRecognizers.Add(panGesture);
            }
        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    // Store the current scale factor applied to the wrapped user interface element,
                    // and zero the components for the center point of the translate transform.
                    startScale = Content.Scale;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;

                    break;

                case GestureStatus.Running:
                    // Calculate the scale factor to be applied.
                    currentScale += (e.Scale - 1) * startScale;
                    currentScale = Math.Max(1, currentScale);

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the X pixel coordinate.
                    double renderedX = Content.X + xOffset;
                    double deltaX = renderedX / Width;
                    double deltaWidth = Width / (Content.Width * startScale);
                    double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the Y pixel coordinate.
                    double renderedY = Content.Y + yOffset;
                    double deltaY = renderedY / Height;
                    double deltaHeight = Height / (Content.Height * startScale);
                    double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                    // Calculate the transformed element pixel coordinates.
                    double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                    double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                    // Apply translation based on the change in origin.
                    Content.TranslationX = Math.Min(0, Math.Max(targetX, -Content.Width * (currentScale - 1)));
                    Content.TranslationY = Math.Min(0, Math.Max(targetY, -Content.Height * (currentScale - 1)));

                    // Apply scale factor.
                    Content.Scale = currentScale;

                    break;

                case GestureStatus.Completed:
                    // Store the translation delta's of the wrapped user interface element.
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;

                    break;
            }
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    startX = e.TotalX;
                    startY = e.TotalY;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;

                    break;

                case GestureStatus.Running:
                    var maxTranslationX = Content.Scale * Content.Width - Content.Width;
                    Content.TranslationX = Math.Min(0, Math.Max(-maxTranslationX, xOffset + e.TotalX - startX));

                    var maxTranslationY = Content.Scale * Content.Height - Content.Height;
                    Content.TranslationY = Math.Min(0, Math.Max(-maxTranslationY, yOffset + e.TotalY - startY));

                    break;

                case GestureStatus.Completed:
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;

                    break;
            }
        }
    }
}
