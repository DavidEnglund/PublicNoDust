﻿using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane : ContentView
	{

		public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(AccordionPane), null, propertyChanged: OnTitleChanged);

		private AccordionHeader header;
		private AccordionView owner;

		public AccordionPane(string title, string image)
		{
            BackgroundColor = Color.Transparent;
			this.header = new AccordionHeader(this);
			this.header.IconImage = ImageSource.FromFile(image);
			this.owner = null;

			Title = title;
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
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
	}
}

