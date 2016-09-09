using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class TrafficPane : AccordionPane
	{
		public TrafficPane() : base("Traffic")
		{
			InitializeComponent();
		}

		public void OnWeatherButtonClicked(object sender, EventArgs args)
		{
			Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Weather"]);
		}

		public void OnLocationAreaButtonClicked(object sender, EventArgs args)
		{
			Owner.VisitPane(Owner.Panes["Weather"], Owner.Panes["LocationArea"]);
		}
	}
}
