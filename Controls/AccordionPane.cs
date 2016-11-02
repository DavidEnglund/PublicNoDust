using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane : ContentView
	{
		public class AccordionHeader : ContentView
		{
			private RelativeLayout titleLayout;
			private StackLayout imageHolder;
			private Label titleLabel;
			private Image iconImage;


			public AccordionHeader(AccordionPane pane)
			{
				HeightRequest = 50;
				HorizontalOptions = LayoutOptions.FillAndExpand;


				Content = (titleLayout = new RelativeLayout()
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.FillAndExpand
				});

				titleLayout.Children.Add((imageHolder = new StackLayout()
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					WidthRequest = 60
				}), heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

				StackLayout imageContainer;
				imageHolder.Children.Add(imageContainer = new StackLayout()
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(20, 0, 0, 0)
				});

				imageContainer.Children.Add((iconImage = new Image()
				{
					HeightRequest = 32,
					HorizontalOptions = LayoutOptions.Center,
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


		public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(AccordionPane), "", propertyChanged: OnTitleChanged);
		public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(string), typeof(AccordionPane), null, propertyChanged: OnImageChanged);
		public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create("IsExpanded", typeof(bool), typeof(AccordionPane), false);

		private AccordionHeader header;
		private AccordionView owner;

		public event EventHandler PaneExpaneded;
		public event EventHandler PaneCollapsed;
		public event EventHandler PaneInvalidated;

		public AccordionPane()
		{
            BackgroundColor = Color.Transparent;
			this.header = new AccordionHeader(this);
			this.owner = null;
		}

		public AccordionPane(string title, string image) : this()
		{
			Title = title;
			Image = image;
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public string Image
		{
			get { return (string)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}

		public bool IsExpanded
		{
			get { return (bool)GetValue(IsExpandedProperty); }
			set { SetValue(IsExpandedProperty, value); }
		}

		public AccordionHeader Header
		{
			get { return this.header; }
		}

		public AccordionView Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}

		private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var accordion = (AccordionPane)bindable;

			if (oldValue != newValue)
			{
				accordion.Header.Title = (string)newValue;
			}
		}

		private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var acccordion = (AccordionPane)bindable;

			if (oldValue != newValue)
			{
				acccordion.Header.IconImage = (string)newValue;
			}
		}

		public virtual void OnPaneExpanded() { }

        public virtual void OnPaneCollapsed() { }

        public virtual void OnPaneInvalidate() { }
	}
}

