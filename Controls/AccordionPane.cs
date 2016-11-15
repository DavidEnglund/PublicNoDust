using System;
using Xamarin.Forms;

namespace Dustbuster
{
	/// <summary>
	/// Represents a pane in AccordionView
	/// </summary>
	public class AccordionPane : ContentView
	{	
		/// <summary>
		/// Header of an AccordionPane
		/// </summary>
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

				// Setup the AccordionHeader layout

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

			/// <summary>
			/// Title for the AccordionHeader
			/// </summary>
			public string Title
			{
				get { return titleLabel.Text; }
				set { this.titleLabel.Text = value; }
			}

		
			/// <summary>
			/// Icon for the AccordionHeader
			/// </summary>
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
		
		/// <summary>
		/// The title of the accordion pane
		/// </summary>
		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		
		/// <summary>
		/// The image of the AccordionPane
		/// </summary>
		public string Image
		{
			get { return (string)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}
		
		/// <summary>
		/// The expanded state of the AccordionPane
		/// </summary>
		public bool IsExpanded
		{
			get { return (bool)GetValue(IsExpandedProperty); }
			set { SetValue(IsExpandedProperty, value); }
		}
		
		/// <summary>
		/// The header for the AccordionPane
		/// </summary>
		public AccordionHeader Header
		{
			get { return this.header; }
		}

		/// <summary>
		/// The AccordionView instance that owns the AccordionPane
		/// </summary>
		public AccordionView Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}
		
		/// <summary>
		/// Called when the AccordionPane title is changed.
		/// </summary>
		private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var accordion = (AccordionPane)bindable;

			if (oldValue != newValue)
			{
				accordion.Header.Title = (string)newValue;
			}
		}

		/// <summary>
		/// Called when the AccordionPane image is changed.
		/// </summary>
		private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var acccordion = (AccordionPane)bindable;

			if (oldValue != newValue)
			{
				acccordion.Header.IconImage = (string)newValue;
			}
		}
		
		/// <summary>
		/// Called when the AccordionPane is expanded
		/// </summary>
		public virtual void OnPaneExpanded() { }

		/// <summary>
		/// Called when the AccordionPane is collapsed
		/// </summary>
       		public virtual void OnPaneCollapsed() { }

		
		/// <summary>
		/// Called when the AccordionPane is invalidated (refresh)
		/// </summary>
        	public virtual void OnPaneInvalidate() { }
	}
}

