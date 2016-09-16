using System;
using Dustbuster.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class TrafficPane : AccordionPane
	{
		//init traffic button group
		SelectButtonGroup trafficButtonGroup = new SelectButtonGroup();

		public TrafficPane() : base("Traffic", "accordion_icon_traffic_light.png")
		{
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "trafficAccordionStyle");

			//add trafficked and non trafficked button to button group
			trafficButtonGroup.AddButton(traffickedButton);
			trafficButtonGroup.AddButton(nonTraffickedButton);
		}

		//trafficked button click
		public void traffickedButton_clicked(object sender, EventArgs e)
		{
			trafficAnswer.Text = "Yes, my area is a trafficked area";
			Title = "Trafficked Area";

			if (!Owner.IsPaneVisited(Owner.Panes["Weather"]))
			{
				Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Weather"]);
			}
		}

		//non trafficked button click
		public void nonTraffickedButton_clicked(object sender, EventArgs e)
		{
			trafficAnswer.Text = "No, my area is not a trafficked area";
			Title = "Non Trafficked Area";

			if (!Owner.IsPaneVisited(Owner.Panes["Weather"]))
			{
				Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Weather"]);
			}
		}
	}
}
