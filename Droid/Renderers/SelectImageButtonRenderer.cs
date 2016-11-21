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
		// I need to create some color and width values here so they can be used in the touch event but set in the properteychanged even
		private Android.Graphics.Color droidBackgroundColor;
		private Android.Graphics.Color droidBorderColor;
		private int droidBorderWidth;

		private Android.Graphics.Color droidHoldBackgroundColor;
		private Android.Graphics.Color droidHoldBorderColor;
		private int droidHoldBorderWidth;

		//adding an android touch recogniser that can detect more touch and hold events than the forms one
		public override bool OnTouchEvent(MotionEvent e)
		{
			// this boolean will be used to decide on the colors at the end and set by the various touch event types
			bool touchDown = false;
			// color values to chnage on hold or relese
			Android.Graphics.Color touchBackgroundColor = droidBackgroundColor;
			Android.Graphics.Color touchBorderColor = droidBorderColor;
			int touchBorderWidth;
			if (e.Action == MotionEventActions.Down)
			{
				
				touchDown = true;

			}
			if (e.Action == MotionEventActions.Up)
			{
				
				touchDown = false;

			}
			if (e.Action == MotionEventActions.ButtonPress)
			{
				
				touchDown = false;
			}
			if (e.Action == MotionEventActions.ButtonRelease)
			{
				
				touchDown = false;
			}
			
			// set the color depebding on the pressed state of the button
			if (touchDown)
			{
				touchBackgroundColor = droidHoldBackgroundColor;
				touchBorderColor = droidHoldBorderColor;
				touchBorderWidth = droidHoldBorderWidth;
			}
			else
			{
				touchBackgroundColor = droidBackgroundColor;
				touchBorderColor = droidBorderColor;
				touchBorderWidth = droidBorderWidth;
			}

			// create a drawable color and set its radius, color, and border
			var rad = new global::Android.Graphics.Drawables.GradientDrawable();
			rad.SetCornerRadius(1000.00F);
			rad.SetColor(touchBackgroundColor);
			rad.SetStroke(touchBorderWidth, touchBorderColor);
			// apply the drawable to the background
			this.Background = rad;
			return base.OnTouchEvent(e);
		}

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
			// get the selected state and hold colors and widths and set them to be used thoughout the renderer
			droidBackgroundColor = formControl.BackgroundColor.ToAndroid();
			droidBorderColor = formControl.BorderColor.ToAndroid();
			droidBorderWidth = formControl.BorderWidth * 2;

			droidHoldBackgroundColor = formControl.HoldBackgroundColor.ToAndroid();
			droidHoldBorderColor = formControl.HoldBorderColor.ToAndroid();
			droidHoldBorderWidth = formControl.HoldBorderWidth * 2;

			base.OnElementPropertyChanged(formControl, e);

			// test of setting the padding for this before drawing a background
			this.SetClipChildren(true);

			// create a drawable color and set irs raduis, color, and border
			var rad = new global::Android.Graphics.Drawables.GradientDrawable();
			rad.SetCornerRadius(1000.00F);
			rad.SetColor(droidBackgroundColor);
			rad.SetStroke(droidBorderWidth, droidBorderColor);
			// apply the drawable to the background
			this.Background = rad;

			// set the draw border
			drawBorderWidth = (float)formControl.BorderWidth * 2;
		}
		// this will clip the image to the circle inside the border and the padding using the padding set above
		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
			Path clipPath = new Path();
			RectF converterRect = new RectF(drawBorderWidth, drawBorderWidth, this.Width - drawBorderWidth, this.Height - drawBorderWidth);
			//float[] rounding = new float[] { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 };
			float[] rounding = new float[] { this.Width, this.Height, this.Width, this.Height, this.Width, this.Height, this.Width, this.Height };
			clipPath.AddRoundRect(converterRect, rounding, Path.Direction.Ccw);
			canvas.ClipPath(clipPath);
		}
	}
}