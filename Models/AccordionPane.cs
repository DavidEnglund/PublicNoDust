using System;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionPane
	{
		private string title;
		private ContentView content;

		public AccordionPane(string title, ContentView content)
		{
			this.title = title;
			this.content = content;
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		public ContentView Content
		{
			get { return this.content; }
			set { this.content = value; }
		}
	}
}

