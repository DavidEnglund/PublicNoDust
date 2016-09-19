using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dustbuster
{
	public class AccordionViewModel : INotifyPropertyChanged
	{
		private Dictionary<string, AccordionPane> accordionPanes;
		private AccordionPane expandedPane;

		public event PropertyChangedEventHandler PropertyChanged;

		public AccordionViewModel()
		{
			accordionPanes = new Dictionary<string, AccordionPane>()
			{
				{ "Traffic", new TrafficPane() },
				{ "Calendar", new CalendarPane() },
				{ "Weather", new WeatherPane() },
				{ "LocationArea", new LocationAreaPane() },
			};

			this.CalculateArea = new Command((nothing) =>
			{
				// Add the key to the input string.

				double tempLength = Length;
				double tempWidth = Width;

				tempLength = (LengthUnit == Units.Kilometre) ? tempLength * 1000 : tempLength;
				tempWidth = (WidthUnit == Units.Kilometre) ? tempWidth * 1000 : tempWidth;

				double tempArea = tempLength * tempWidth;

				Area = (AreaUnit == Units.Kilometre) ? tempArea / 1000000 : tempArea;

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

		public Dictionary<string, AccordionPane> AccordionPanes
		{
			get { return this.accordionPanes; }
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

		public ICommand CalculateArea { set; get; }

		public ICommand ChangeLengthUnit { set; get; }

		public ICommand ChangeWidthUnit { set; get; }

		public ICommand ChangeAreaUnit { set; get; }
	}
}

