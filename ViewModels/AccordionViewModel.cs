using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionViewModel : INotifyPropertyChanged
	{
		private List<AccordionPane> accordionPanes;
		private AccordionPane expandedPane;

		public event PropertyChangedEventHandler PropertyChanged;

		public AccordionViewModel()
		{
			AccordionPane trafficPane = new AccordionPane("Traffic", new ContentView()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Gray
			});

			accordionPanes = new List<AccordionPane>()
			{
				trafficPane
			};
		}

		public List<AccordionPane> AccordionPanes
		{
			get { return this.accordionPanes; }
		}

		public AccordionPane ExpandedPane
		{
			get { return this.expandedPane; }
			set
			{
				this.expandedPane = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ExpandedPane"));
				}
			}
		}
	}
}

