using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using System.ComponentModel;
using Android.Graphics;
using Dustbuster;
using Dustbuster.Droid;

[assembly: ExportRenderer(typeof(Dustbuster.SelectImageButton), typeof(SelectImageButtonRenderer))]
namespace Dustbuster.Droid
{
    public class SelectImageButtonRenderer : ViewRenderer
    {
        public SelectImageButtonRenderer()
        {
            SetPadding(0, 0, 0, 0);           
        }
        //setting a double here that I can set on property chnage with the border width and then use in the draw
        private float drawBorderWidth = 0;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {           
            // get a copy of the xamarin control we are changing
            SelectImageButton formControl = (SelectImageButton)sender;
            
            base.OnElementPropertyChanged(formControl, e);

            // test of setting the padding for this before drawing a background
            this.SetClipChildren(true);
            // get the set background color
            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;
            // create a drawable color and set irs raduis, color, and border
            var rad = new global::Android.Graphics.Drawables.GradientDrawable();
            rad.SetCornerRadius(1000.00F);
            rad.SetColor(bgcolor.ToAndroid());
            rad.SetStroke(formControl.BorderWidth*2,formControl.BorderColor.ToAndroid());
            // apply the drawable to the background
            this.Background = rad;
            
            // set the draw border
            drawBorderWidth = (float)formControl.BorderWidth*2;
        }
        // this will clip the image to the circle inside the border and the padding using the padding set above
        protected override void OnDraw(Canvas canvas)
        {    
            base.OnDraw(canvas);
            Path clipPath = new Path();
            RectF converterRect = new RectF(drawBorderWidth,drawBorderWidth, this.Width - drawBorderWidth, this.Height - drawBorderWidth);
            //float[] rounding = new float[] { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 };
            float[] rounding = new float[] { this.Width, this.Height, this.Width, this.Height, this.Width, this.Height, this.Width, this.Height };
            clipPath.AddRoundRect(converterRect,rounding,Path.Direction.Ccw );
            canvas.ClipPath(clipPath);
        }
    }
}