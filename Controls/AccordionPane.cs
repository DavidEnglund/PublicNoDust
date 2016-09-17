using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane : ContentView
	{

		public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(AccordionPane), null, propertyChanged: OnTitleChanged);
		public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(string), typeof(AccordionPane), null, propertyChanged: OnImageChanged);

		private AccordionHeader header;
		private AccordionView owner;

		public AccordionPane(string title, string image)
		{
            BackgroundColor = Color.Transparent;
			this.header = new AccordionHeader(this);
			this.header.IconImage = ImageSource.FromFile(image);
			this.owner = null;

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
	}
}

