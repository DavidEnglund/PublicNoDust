using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionHeader : ContentView
	{
		private Label titleLabel;

		public AccordionHeader(AccordionPane pane)
		{
			BackgroundColor = Color.Green;
			HeightRequest = 50;
			HorizontalOptions = LayoutOptions.FillAndExpand;
			Content = (titleLabel = new Label()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20f
			});
		}

		public string Title
		{
			get { return titleLabel.Text; }
			set { this.titleLabel.Text = value; }
		}
	}
}

