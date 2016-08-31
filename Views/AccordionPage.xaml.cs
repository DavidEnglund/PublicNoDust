using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dustbuster
{
	public partial class AccordionPage : ContentPage
	{
		private AccordionViewModel accordionViewModel;

		public AccordionPage()
		{
			accordionViewModel = new AccordionViewModel();
			BindingContext = accordionViewModel;

			InitializeComponent();

			CreateAccordion();
		}

		protected void CreateAccordion()
		{
			foreach (AccordionPane accordionPane in accordionViewModel.AccordionPanes)
			{
				View accordionContent = accordionPane.Content;
				accordionContent.IsVisible = false;

				if (accordionContent != null)
				{
					ContentView accordionHeader = new ContentView()
					{
						BackgroundColor = Color.Green,
						HeightRequest = 50,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Content = new Label()
						{
							Text = accordionPane.Title,
							HorizontalTextAlignment = TextAlignment.Center,
							VerticalTextAlignment = TextAlignment.Center,
							FontSize = 20f
						}
					};

					AccordionView.Children.Add(accordionHeader);
					AccordionView.Children.Add(accordionContent);

					accordionHeader.GestureRecognizers.Add(new TapGestureRecognizer()
					{
						Command = new Command(() => {
							if (accordionViewModel.ExpandedPane != null)
							{
								accordionViewModel.ExpandedPane.Content.IsVisible = false;
							}

							accordionViewModel.ExpandedPane = accordionPane;
							accordionViewModel.ExpandedPane.Content.IsVisible = true;	
						})
					});
				}
			}
		}
	}
}

