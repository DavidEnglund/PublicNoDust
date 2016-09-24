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

		public TrafficPane() //: base("Traffic", "unselectedNone.png")
		{
			InitializeComponent();

			Header.SetDynamicResource(StyleProperty, "trafficAccordionStyle");

			//add trafficked and non trafficked button to button group
			trafficButtonGroup.AddButton(traffickedButton);
			trafficButtonGroup.AddButton(nonTraffickedButton);

			//set traficOption to none
			App.TrafficOption = TrafficOptions.None;
		}

		//trafficked button click
		public void traffickedButton_clicked(object sender, EventArgs e)
		{
			trafficAnswer.Text = "Yes, my area is a trafficked area";
			Title = "Trafficked Area";
			Image = "accordion_icon_traffic_light.png";

			if (App.TrafficOption != TrafficOptions.TraffickedArea)
			{
				/*if (App.TrafficOption == TrafficOptions.NonTraffickedArea)
				{
					Owner.Panes["Calendar"].OnPaneInvalidate();
				}*/

				//set the option enum
				App.TrafficOption = TrafficOptions.TraffickedArea;
				App.WeatherOption = WeatherOptions.None;
				App.DurationOption = DurationOptions.None;
				//visit calendar pane

				/*if (Owner.IsPaneVisited(Owner.Panes["Weather"]))
				{
					Owner.VisitPane(Owner.Panes["Weather"], Owner.Panes["Calendar"]);
				}
				else {
					Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Calendar"]);
				}*/
			}
		}

		//non trafficked button click
		public void nonTraffickedButton_clicked(object sender, EventArgs e)
		{
			trafficAnswer.Text = "No, my area is not a trafficked area";
			Title = "Non Trafficked Area";
			Image = "accordion_icon_traffic_openroad.png";

			/*if (App.TrafficOption != TrafficOptions.NonTraffickedArea)
			{
				if (App.TrafficOption == TrafficOptions.TraffickedArea)
				{
					Owner.Panes["Calendar"].OnPaneInvalidate();
				}

				//set the option enum
				App.TrafficOption = TrafficOptions.NonTraffickedArea;
				App.WeatherOption = WeatherOptions.None;
				App.DurationOption = DurationOptions.None;
				//visit calendar pane
				Owner.VisitPane(Owner.Panes["LocationArea"], Owner.Panes["Calendar"]);
			}*/
		}
	}
}