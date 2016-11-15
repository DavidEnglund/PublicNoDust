using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Dustbuster.iOS;
using System.ComponentModel;
using UIKit;
using Dustbuster;
using CoreGraphics;
using CoreAnimation;

[assembly: ExportRenderer(typeof(Dustbuster.SelectImageButton), typeof(SelectImageButtonRenderer))]
namespace Dustbuster.iOS
{
    public class SelectImageButtonRenderer : ViewRenderer
    {
        // variables to hold the colors and borders for use in this renderer
        private CGColor iosBackgroundColor;
        private CGColor iosBorderColor;
        private int iosBorderWidth;
        private CGColor iosHoldBackgroundColor;
        private CGColor iosHoldBorderColor;
        private int iosHoldBorderWidth;
        // the eventHandler from the forms code that will be runn at the end of a long press
        EventHandler buttonevents;
        // the forms round control
        SelectImageButton formControl;

        // the long press gesture action code
        private void holdPress(UILongPressGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                Layer.BackgroundColor = iosHoldBackgroundColor;
                Layer.BorderColor = iosHoldBorderColor;
                Layer.BorderWidth = iosHoldBorderWidth;
            }
            if (gestureRecognizer.State == UIGestureRecognizerState.Ended)
            {
                Layer.BackgroundColor = iosBackgroundColor;
                Layer.BorderColor = iosBorderColor;
                Layer.BorderWidth = iosBorderWidth;
                buttonevents.Invoke(formControl, new EventArgs());
            }
        }

        // this will draw background and circular border then it will cut the image to a circle inside the border and padding
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            // the control as it exists in the protable project
            formControl = (SelectImageButton)sender;
            // the click events form the forms control to be added to the end of the long click
            buttonevents = formControl.ClickEvents; 
            // setting the colors for use in ther renderer outside of the property changed event
            iosBackgroundColor = formControl.BackgroundColor.ToCGColor();
            iosBorderColor = formControl.BorderColor.ToCGColor();
            iosBorderWidth = formControl.BorderWidth;
            iosHoldBackgroundColor = formControl.HoldBackgroundColor.ToCGColor();
            iosHoldBorderColor = formControl.HoldBorderColor.ToCGColor();
            iosHoldBorderWidth = formControl.HoldBorderWidth;
            // background
            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;
            Layer.BackgroundColor = bgcolor.ToCGColor();
            // border
            Layer.BorderColor = formControl.BorderColor.ToCGColor();
            Layer.BorderWidth = formControl.BorderWidth;

            Layer.CornerRadius = (float)formControl.Width/2;
            // setup for a long press gesture
            UILongPressGestureRecognizer holdGesture = new UILongPressGestureRecognizer(holdPress);
            holdGesture.MinimumPressDuration = 0.0;
            this.AddGestureRecognizer(holdGesture);
            // this need to check that the control is actually loaded as it can call this method without the control present
            if (formControl != null)
            {
                
                // a top left and bottom right adjustment for the clipping border
                float TLborderControl = 0 ;
                float BRborderControl = formControl.BorderWidth;

                // a rectangle made usiing the bounds and borders of the control that we will use the cut an elipse with
                CGRect clipRect = new CGRect();

                clipRect.X = (float)0 + TLborderControl;
                clipRect.Y = (float)0 + TLborderControl;
                clipRect.Width = (float)Element.Bounds.Width;
                clipRect.Height = (float)Element.Bounds.Height;

                CAShapeLayer clipShapeLayer = new CAShapeLayer();                
                CGPath clipPath = new CGPath();
                
                clipPath.AddEllipseInRect(clipRect);
                clipShapeLayer.Path = clipPath;
                Layer.Mask = clipShapeLayer;
            }
        }       
     }
}
