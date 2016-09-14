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

			//add trafficked and non trafficked button to button group
			trafficButtonGroup.AddButton(traffickedButton);
			trafficButtonGroup.AddButton(nonTraffickedButton);
			civilOrMiningColours("mining");
		}

		//choose civil or mining colour options
		public void civilOrMiningColours(string input)
		{
			if (input == "civil")
			{
				//green
				nonTraffickedButton.UnselectedBackgroundColor = Color.FromHex("78dd9c");
				nonTraffickedButton.SelectedBackgroundColor = Color.FromHex("18b750");
				traffickedButton.UnselectedBackgroundColor = Color.FromHex("78dd9c");
				traffickedButton.SelectedBackgroundColor = Color.FromHex("18b750");
			}
			else
			{
				//blue
				nonTraffickedButton.UnselectedBackgroundColor = Color.FromHex("47b6d9");
				nonTraffickedButton.SelectedBackgroundColor = Color.FromHex("079ece");
				traffickedButton.UnselectedBackgroundColor = Color.FromHex("47b6d9");
				traffickedButton.SelectedBackgroundColor = Color.FromHex("079ece");
			}
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

		//public void OnLocationAreaButtonClicked(object sender, EventArgs args)
		//{
		//	Owner.VisitPane(Owner.Panes["Weather"], Owner.Panes["LocationArea"]);
		//	Title = Entry.Text;
		//}
	}
}
