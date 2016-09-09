using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionHeader : ContentView
	{
		private AccordionPane pane;

		public AccordionHeader(AccordionPane pane)
		{
			this.pane = pane;
			BackgroundColor = Color.Green;
			HeightRequest = 50;
			HorizontalOptions = LayoutOptions.FillAndExpand;
			Content = new Label()
			{
				Text = pane.Title,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20f
			};
		}
	}
}

