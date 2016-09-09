using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionViewModel : INotifyPropertyChanged
	{
		private Dictionary<string, AccordionPane> accordionPanes;
		private AccordionPane expandedPane;

		public event PropertyChangedEventHandler PropertyChanged;

		public AccordionViewModel()
		{
			accordionPanes = new Dictionary<string, AccordionPane>()
			{
				{ "Traffic", new TrafficPane() },
				{ "Calendar", new CalendarPane() },
				{ "Weather", new WeatherPane() },
				{ "LocationArea", new LocationAreaPane() },
			};
		}

		public Dictionary<string, AccordionPane> AccordionPanes
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

