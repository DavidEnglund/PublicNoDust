using System;
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
			Image = "accordion_icon_traffic_light.png";

			if (App.TrafficOption != TrafficOptions.TraffickedArea)
			{
				//set the option enum
				App.TrafficOption = TrafficOptions.TraffickedArea;
				//visit calendar pane
				Owner.VisitPane(Owner.Panes["Calendar"], Owner.Panes["Calendar"]);
			}
		}

		//non trafficked button click
		public void nonTraffickedButton_clicked(object sender, EventArgs e)
		{
			trafficAnswer.Text = "No, my area is not a trafficked area";
			Title = "Non Trafficked Area";
			Image = "accordion_icon_traffic_openroad.png";

			if (App.TrafficOption != TrafficOptions.NonTraffickedArea)
			{
				//set the option enum
				App.TrafficOption = TrafficOptions.NonTraffickedArea;
				//visit calendar pane
				Owner.VisitPane(Owner.Panes["Calendar"], Owner.Panes["Calendar"]);
			}
		}
	}
}