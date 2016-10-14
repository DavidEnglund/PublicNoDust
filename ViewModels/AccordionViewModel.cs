using Dustbuster.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Dustbuster.Views;
namespace Dustbuster
{
	public class AccordionViewModel : INotifyPropertyChanged
	{
		private AccordionPane expandedPane;
		private TrafficPane trafficPane;
		private CalendarPane calendarPane;
		private WeatherPane weatherPane;
		private LocationAreaPane locationAreaPane;

		public event PropertyChangedEventHandler PropertyChanged;

        Analytics AnalyticsClass = new Analytics();

		public AccordionViewModel(INavigation navigation)
		{
			trafficPane = null;
			calendarPane = null;
			weatherPane = null;
			locationAreaPane = null;

			this.Calculate = new Command(async (nothing) =>
			{
				double tempLength = Length;
				double tempWidth = Width;

				tempLength = (LengthUnit == Units.Kilometre) ? tempLength * 1000 : tempLength;
				tempWidth = (WidthUnit == Units.Kilometre) ? tempWidth * 1000 : tempWidth;

				double tempArea = tempLength * tempWidth;

				Area = (AreaUnit == Units.Kilometre) ? tempArea / 1000000 : tempArea;

				ProductViewModel productViewModel = new ProductViewModel(this);

				DbConnectionManager connection = App.ProductsDb;

				var productSelection = connection.SelectProducts((int)App.DurationOption,
										  (int)App.TrafficOption,
										  (App.WeatherOption == WeatherOptions.RainExpected));

				foreach (ProductMatrix productMatrix in productSelection)
				{
					ProductDescription productDescription = connection.GetProductInfo(productMatrix);

					productViewModel.Products.Add(productDescription);
                    if (productViewModel.Products.Count > 0)
                    {
                        productViewModel.SelectedProduct = productViewModel.Products[0];
                    }
				}

                //Starts Analytics up
                AnalyticsClass.SetDetails();
                //Send all needed data
                AnalyticsClass.SendAnalytics(App.IndustryOption.ToString(), "Traffic", App.TrafficOption.ToString(), "Calendar", App.DurationOption.ToString(), "Rain", App.WeatherOption.ToString(), "Location", " ");

				await navigation.PushAsync(new ProductPage(productViewModel));
			});

			this.ChangeLengthUnit = new Command((nothing) =>
			{
				if (LengthUnit == Units.Metre)
				{
					LengthUnit = Units.Kilometre;
					Length = Length / 1000;
				}
				else
				{
					LengthUnit = Units.Metre;
					Length = Length * 1000;
				}
			});

			this.ChangeWidthUnit = new Command((nothing) =>
			{
				if (WidthUnit == Units.Metre)
				{
					WidthUnit = Units.Kilometre;
					Width = Width / 1000;
				}
				else
				{
					WidthUnit = Units.Metre;
					Width = Width * 1000;
				}
			});

			this.ChangeAreaUnit = new Command((nothing) =>
			{
				if (AreaUnit == Units.Metre)
				{
					AreaUnit = Units.Kilometre;
					Area = Area / 1000000;
				}
				else
				{
					AreaUnit = Units.Metre;
					Area = Area * 1000000;
				}
			});
		}

		//enum civil mining getter and setter
		public IndustryOptions industryOption { get; set; }

		public TrafficPane TrafficPane
		{
			get { return trafficPane; }
			set { trafficPane = value; }
		}

		public CalendarPane CalendarPane
		{
			get { return calendarPane; }
			set { calendarPane = value; }
		}

		public WeatherPane WeatherPane
		{
			get { return weatherPane; }
			set { weatherPane = value; }
		}

		public LocationAreaPane LocationAreaPane
		{
			get { return locationAreaPane; }
			set { locationAreaPane = value; }
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

		private double length;
		private double width;
		private double area;

		private Units lengthUnit;
		private Units widthUnit;
		private Units areaUnit;

		private readonly static string METRE_NOTATION = "m", KILOMETRE_NOTATION = "km";
		private readonly static string METRE_SQUARE_NOTATION = "m2", KILOMETRE_SQUARE_NOTATION = "km2";

		public double Length
		{
			get
			{
				return length;
			}
			set
			{
				if (length != value)
				{
					length = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Length"));
					}
				}
			}
		}

		public double Width
		{
			get
			{
				return width;
			}
			set
			{
				if (width != value)
				{
					width = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Width"));
					}
				}
			}
		}

		public double Area
		{
			get
			{
				return area;
			}
			set
			{
				if (area != value)
				{
					area = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Area"));
					}
				}
			}
		}

		public Units LengthUnit
		{
			get
			{
				return lengthUnit;
			}
			set
			{
				if (lengthUnit != value)
				{
					lengthUnit = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("LengthUnit"));
					}
				}
			}
		}

		public Units WidthUnit
		{
			get
			{
				return widthUnit;
			}
			set
			{
				if (widthUnit != value)
				{
					widthUnit = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("WidthUnit"));
					}
				}
			}
		}

		public Units AreaUnit
		{
			get
			{
				return areaUnit;
			}
			set
			{
				if (areaUnit != value)
				{
					areaUnit = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("AreaUnit"));
					}
				}
			}
		}

		public ICommand Calculate { set; get; }

		public ICommand ChangeLengthUnit { set; get; }

		public ICommand ChangeWidthUnit { set; get; }

		public ICommand ChangeAreaUnit { set; get; }
	}
}

