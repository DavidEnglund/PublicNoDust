using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionHeader : ContentView
	{
		private RelativeLayout titleLayout;
		private StackLayout imageHolder;
		private Label titleLabel;
		private Image iconImage;


		public AccordionHeader(AccordionPane pane)
		{
			BackgroundColor = Color.Green;
			HeightRequest = 50;
			HorizontalOptions = LayoutOptions.FillAndExpand;

	
			Content = (titleLayout = new RelativeLayout()
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			});

			titleLayout.Children.Add((imageHolder = new StackLayout()
			{
				Padding = new Thickness(40, 0, 0, 0)
			}), Constraint.Constant(100));

			imageHolder.Children.Add((iconImage = new Image()
			{
				HeightRequest = 32
			}));
			titleLayout.Children.Add((titleLabel = new Label()
			{

				FontSize = 20f,
				TextColor = Color.White,
				FontAttributes = FontAttributes.Bold
			}), Constraint.Constant(100));
		}

		public string Title
		{
			get { return titleLabel.Text; }
			set { this.titleLabel.Text = value; }
		}

		public ImageSource IconImage
		{
			get { return this.iconImage.Source; }
			set { this.iconImage.Source = value; }
		}
	}
}

