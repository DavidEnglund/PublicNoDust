using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane : ContentView
	{
		private string title;
		private AccordionHeader header;
		private AccordionView owner;

		public AccordionPane(string title)
		{
			this.title = title;
			this.header = new AccordionHeader(this);
			this.owner = null;
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
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
	}
}

