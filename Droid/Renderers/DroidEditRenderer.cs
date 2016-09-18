using System;
using Dustbuster.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Entry), typeof(DroidEditRenderer))]
namespace Dustbuster.Droid
{
	public class DroidEditRenderer : EntryRenderer
	{
		public DroidEditRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetBackgroundColor(AndroidColor.Transparent);
			}
		}
	}
}
