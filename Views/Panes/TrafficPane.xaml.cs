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
		SelectButtonGroup myButtonGroup = new SelectButtonGroup();

		public TrafficPane() : base("Traffic")
		{
			InitializeComponent();
			myButtonGroup.AddButton(trafficedButton);
			myButtonGroup.AddButton(nonTrafficedButton);
			trafficedButton.Selected = false;
			nonTrafficedButton.Selected = false;
		}


		//public void OnWeatherButtonClicked(object sender, EventArgs args)
		//{
		//	Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Weather"]);
		//	Title = Entry.Text;
		//}

		//public void OnLocationAreaButtonClicked(object sender, EventArgs args)
		//{
		//	Owner.VisitPane(Owner.Panes["Weather"], Owner.Panes["LocationArea"]);
		//	Title = Entry.Text;
		//}
	}
}
