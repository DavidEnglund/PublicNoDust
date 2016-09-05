using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionViewModel
	{
		private List<AccordionPane> accordionPanes;
		private AccordionPane expandedPane;

		public AccordionViewModel()
		{
			accordionPanes = new List<AccordionPane>();
			expandedPane = null;

            accordionPanes.Add(new AccordionPane("Traffic", new ContentView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            }));
			accordionPanes.Add(new AccordionPane("Duration", new ContentView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            }));
			accordionPanes.Add(new AccordionPane("Weather", new ContentView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            }));
			accordionPanes.Add(new AccordionPane("Location and Area", new ContentView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            }));
		}

		public List<AccordionPane> AccordionPanes
		{
			get { return this.accordionPanes; }
		}

		public AccordionPane ExpandedPane
		{
			get { return this.expandedPane; }
			set { this.expandedPane = value; }
		}
	}
}

