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
        // this will draw backgournd and circular border then it will cut the image to a circle inside the border and padding
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            // the control as it exists in the protable project
            SelectImageButton formControl = (SelectImageButton)sender;
            // background
            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;
            Layer.BackgroundColor = bgcolor.ToCGColor();
            // border
            Layer.BorderColor = formControl.BorderColor.ToCGColor();
            Layer.BorderWidth = formControl.BorderWidth;

            Layer.CornerRadius = (float)formControl.Width/2;    

            // this need to check that the control is actually loaded as it can call this method without the control present
            if(formControl != null)
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
