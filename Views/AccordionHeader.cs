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
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            });

            titleLayout.Children.Add((imageHolder = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 0, 0, 0)
            }), widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            imageHolder.Children.Add((iconImage = new Image()
            {
                HeightRequest = 32,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand
            }));
            titleLayout.Children.Add((titleLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20f,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold
            }), widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));
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

