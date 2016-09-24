﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Dustbuster
{
	public class AccordionView : StackLayout
	{
		public static readonly BindableProperty ExpandedPaneProperty = BindableProperty.Create("ExpandedPane", typeof(AccordionPane), typeof(AccordionView), null, BindingMode.TwoWay, propertyChanged: OnExpandedPaneChanged);

		private StackLayout prevPanes;
		private StackLayout nextPanes;
		private StackLayout currentPane;
		private List<AccordionPane> visitedPanes;

		private ObservableCollection<AccordionPane> panes;

		public AccordionView()
		{
			prevPanes = new StackLayout();
			prevPanes.VerticalOptions = LayoutOptions.Start;
			nextPanes = new StackLayout();
			nextPanes.VerticalOptions = LayoutOptions.End;
			currentPane = new StackLayout();
			currentPane.VerticalOptions = LayoutOptions.FillAndExpand;
            currentPane.BackgroundColor = Color.Transparent;

            //Removes spacing between Accordion Headers
            prevPanes.Spacing = 0;
            nextPanes.Spacing = 0;

			this.Children.Add(prevPanes);
			this.Children.Add(currentPane);
			this.Children.Add(nextPanes);

			visitedPanes = new List<AccordionPane>();

			panes = new ObservableCollection<AccordionPane>();
			panes.CollectionChanged += (sender, e) =>
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						
						foreach (AccordionPane pane in e.NewItems.OfType<AccordionPane>())
						{
							if (pane.Owner == null)
							{
								pane.Owner = this;
								pane.BindingContext = this.BindingContext;
								pane.Header.GestureRecognizers.Add(new TapGestureRecognizer()
								{
									Command = new Command(() =>
									{
										ExpandedPane = pane;
									})
								});

								pane.IsVisible = false;

								currentPane.Children.Add(pane);
							}

							if (ExpandedPane == null)
							{
								ExpandedPane = pane;
							}

						}

						break;
				}
			};
		}

		public ObservableCollection<AccordionPane> Panes
		{
			get { return this.panes; }
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


				if (oldExpanded != null)
				{
					oldExpanded.IsVisible = false;
					oldExpanded.OnPaneCollapsed();
				}

				newExpanded.IsVisible = true;
				newExpanded.OnPaneExpanded();

				foreach(AccordionPane pane in visitedPanes)
				{
					if (pane == newExpanded)
					{
						expandedFound = true;

					}
					else {
						if (expandedFound)
						{
							nextPanes.Children.Add(pane.Header);
						}
						else {
							prevPanes.Children.Add(pane.Header);
						}
					}
				}
			}
		}

		public void VisitPane(AccordionPane pane)
		{
			if (pane != null)
			{
				ExpandedPane = pane;
			}
		}

		public void VisitPane(AccordionPane oldPane, AccordionPane newPane)
		{
			if (oldPane != newPane)
			{
				int index;

				if ((index = visitedPanes.IndexOf(oldPane)) >= 0)
				{
					visitedPanes[index] = newPane;

					visitedPanes.RemoveAll((pane) => index < visitedPanes.IndexOf(pane));
				}

				ExpandedPane = newPane;
			}
		}

		public bool IsPaneVisited(AccordionPane pane)
		{
			return visitedPanes.Contains(pane);
		}
	}
}


