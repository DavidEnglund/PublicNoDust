using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane : ContentView
	{

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

        /*
         * this method purposely is made empty or unimplemented to be used by child or inherited class
         * unless new solving could prove this method is not mandatory
         * this method should exist
         */
        public virtual void OnPaneCollapsed() { } // do not remove

        public virtual void OnPaneInvalidate() { }


	}
}

