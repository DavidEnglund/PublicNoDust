using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace Dustbuster
{
	public class AccordionView : StackLayout
	{
		public static readonly BindableProperty PanesProperty = BindableProperty.Create("Panes", typeof(IEnumerable<AccordionPane>), typeof(AccordionPane), null, propertyChanged: OnPanesChanged);
		public static readonly BindableProperty ExpandedPaneProperty = BindableProperty.Create("ExpandedPane", typeof(AccordionPane), typeof(AccordionView), null, BindingMode.TwoWay, propertyChanged: OnExpandedPaneChanged);

		private StackLayout prevPanes;
		private StackLayout nextPanes;
		private StackLayout currentPane;
		private List<AccordionPane> visitedPanes;

		public AccordionView()
		{
			prevPanes = new StackLayout();
			prevPanes.VerticalOptions = LayoutOptions.Start;
			nextPanes = new StackLayout();
			nextPanes.VerticalOptions = LayoutOptions.End;
			currentPane = new StackLayout();
			currentPane.VerticalOptions = LayoutOptions.FillAndExpand;

			this.Children.Add(prevPanes);
			this.Children.Add(currentPane);
			this.Children.Add(nextPanes);

			visitedPanes = new List<AccordionPane>();
		}

		public List<AccordionPane> Panes
		{
			get { return (List<AccordionPane>)GetValue(PanesProperty); }
			set { SetValue(PanesProperty, value); }
		}


		public AccordionPane ExpandedPane
		{
			get { return (AccordionPane)GetValue(ExpandedPaneProperty); }
			set { SetValue(ExpandedPaneProperty, value); }
		}

		private static void OnExpandedPaneChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var accordion = (AccordionView)bindable;

			if (oldValue != newValue)
			{
				accordion.OnExpandedPaneChanged((AccordionPane)oldValue);
			}
		}

		private static void OnPanesChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var accordion = (AccordionView)bindable;

			if (oldValue != newValue)
			{
				accordion.OnPanesChanged();
			}
		}

		private void OnExpandedPaneChanged(AccordionPane oldExpanded)
		{
			bool expandedFound = false;
			if (ExpandedPane != null)
			{
				AccordionPane newExpanded = ExpandedPane;

				if (!visitedPanes.Contains(newExpanded))
				{
					visitedPanes.Add(newExpanded);
				}

				nextPanes.Children.Clear();
				prevPanes.Children.Clear();

				currentPane.Children.Clear();
				currentPane.Children.Add(newExpanded.PaneContent);

				foreach(AccordionPane pane in visitedPanes)
				{
					if (pane == newExpanded)
					{
						expandedFound = true;

					}
					else {
						if (expandedFound)
						{
							nextPanes.Children.Add(pane);
						}
						else {
							prevPanes.Children.Add(pane);
						}
					}
				}
			}
		}

		private void OnPanesChanged()
		{
			ExpandedPane = Panes.First();

			foreach (AccordionPane pane in Panes)
			{
				pane.Owner = this;
				pane.GestureRecognizers.Add(new TapGestureRecognizer()
				{
					Command = new Command(() =>
					{
						ExpandedPane = pane;
					})
				});
			}
		}
	}


	public class AccordionPane : ContentView
	{
		private string title;
		private ContentView paneContent;
		private AccordionView owner;

		public AccordionPane(string title, ContentView paneContent)
		{
			this.title = title;
			this.paneContent = paneContent;
			this.owner = null;


			BackgroundColor = Color.Green;
			HeightRequest = 50;
			HorizontalOptions = LayoutOptions.FillAndExpand;
			Content = new Label()
			{
				Text = title,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20f
			};
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		public ContentView PaneContent
		{
			get { return this.paneContent; }
			set { this.paneContent = value; }
		}

		public AccordionView Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}
	}
}


