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
		private bool calculateTapped;

        public AccordionViewModel(INavigation navigation)
        {
            trafficPane = null;
            calendarPane = null;
            weatherPane = null;
            locationAreaPane = null;

            this.Calculate = new Command(async (nothing) =>
            {
				if (!calculateTapped)
				{
					calculateTapped = true;

					// double check the inputs (length, width, location)
                    if (!validateInputs()) 
                    {
						// terminate the process
                        calculateTapped = false;
                        return; 
                    }


                    Job newJob = new Job()
                    {
                        Area = area,
                        AreaTypeID = (int)App.TrafficOption,
                        CreationDate = DateTime.Now,
                        WillRain = (App.WeatherOption == WeatherOptions.RainExpected),
                        JobType = (int)App.IndustryOption,
                        DurationMaxDays = (int)App.DurationOption,
                        Location = Location
                    };

                    //quick and nasty fix for calc issue. page is still buggy.
                    //job area will be saved as sq metre
                    if (areaUnit == Units.Kilometre)
                    {
                        newJob.Area = newJob.Area * 1000000;
                    }

                    if (Settings.EnableAnalytics)
                    {
                        //Starts Analytics up
                        AnalyticsClass.SetDetails();
                        //Send all needed data
                        AnalyticsClass.SendAnalytics(App.IndustryOption.ToString(), "Traffic", App.TrafficOption.ToString(), "Calendar", App.DurationOption.ToString(), "Rain", App.WeatherOption.ToString(), "Location", Location);
                    }
                    
					App.JobsDb.DbConnection.Insert(newJob);

                    ProductViewModel productViewModel = new ProductViewModel();
					DbConnectionManager productsDB = App.ProductsDb;

					IEnumerable<ProductMatrix> productSelection = productsDB.SelectProducts((int)App.DurationOption,
											                      (int)App.TrafficOption,
											                      (App.WeatherOption == WeatherOptions.RainExpected));
                    
					foreach (ProductMatrix productMatrix in productSelection)
					{
						ProductDescription productDescription = productsDB.GetProductInfo(productMatrix);

						productViewModel.Products.Add(new ProductResult() { Job = newJob, Description = productDescription });
						if (productViewModel.Products.Count > 0)
						{
							productViewModel.SelectedProduct = productViewModel.Products[0];
						}
					}


					await navigation.PushAsync(new ProductPage(productViewModel));

					calculateTapped = false;
				}
            });

            this.ChangeLengthUnit = new Command((nothing) =>
            {
                if (LengthUnit == Units.Metre)
                {
                    LengthUnit = Units.Kilometre;
                }
                else
                {
                    LengthUnit = Units.Metre;
                }
                setArea();
            });

            this.ChangeWidthUnit = new Command((nothing) =>
            {   
                if (WidthUnit == Units.Metre)
                {
                    WidthUnit = Units.Kilometre;
                }
                else
                {
                    WidthUnit = Units.Metre;
                }
                setArea();
               
            });

            this.ChangeAreaUnit = new Command((nothing) =>
            {
                if (AreaUnit == Units.Metre)
                {
                    AreaUnit = Units.Kilometre;
                }
                else
                {
                    AreaUnit = Units.Metre;
                }
               if(areaUntouched == area)
                {
                    setArea();
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

        //private string location;
        private double length;
        private double width;
        private double area;
        private double areaUntouched;
        public double areaInMetres;

        private Units lengthUnit;
        private Units widthUnit;
        private Units areaUnit;

        //this color is for the length and width text. it will change depending on whether the area entry is using them or not
        private Color areaColor = Color.FromHex("#d9d9d9");

        private readonly static string METRE_NOTATION = "m", KILOMETRE_NOTATION = "km";
        private readonly static string METRE_SQUARE_NOTATION = "m2", KILOMETRE_SQUARE_NOTATION = "km2";


        public string Location
        {
            get { return UserInfoPHP.Instance.jobLocation; }
            set
            {
                if (UserInfoPHP.Instance.jobLocation != value)
                {
                    UserInfoPHP.Instance.jobLocation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }
        // this is the publec setter for the test color for width an length to bind to
        public Color AreaColor
        {
            get {
                return areaColor;
            }
            set {
                areaColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AreaColor"));
            }
        }

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
                    setArea();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Length"));
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
                    setArea();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
                }
            }
        }
        /// <summary>
        /// this method will be called by anything that changes the width or length values and set the area accordingly
        /// </summary>
        private void setArea()
        {
            // setting the text feild color back now it being used again #d9d9d9
            AreaColor = Color.FromHex("#d9d9d9");
            // going to get a converted width then a converted length and set the area to that. this way the width and length stays the same but the area adjust to m/ km
            double convertedWidth = width;
            if (WidthUnit == Units.Kilometre)
            {
                convertedWidth *= 1000;
            }
            double convertedLength = length;
            if (LengthUnit == Units.Kilometre)
            {
                convertedLength *= 1000;
            }
            // if area is set to km it needs to receive the data as km so convert metre value to km value whilst keeping it meaning the same thing
            if (AreaUnit == Units.Metre)
            {
                areaUntouched = convertedLength * convertedWidth;
                Area = (decimal)(convertedLength * convertedWidth);
            }
            else
            {
                areaUntouched = (convertedLength * convertedWidth) / 1000000;
                Area = (decimal)(convertedLength * convertedWidth) / 1000000;
            }
        }
        // this is the area that is diplayed and set by changing the area
        public decimal Area
        {
            get
            {
                    return (decimal)area; 
            }
            set
            {
                if (area != (double)value)
                {
                    area = (double)value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area"));
                    // setting the len and width text color to red if they are not being used for the area
                    if(area != areaUntouched)
                    {
                        AreaColor = Color.FromHex("#d9d999");
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

        private bool validateInputs()
        {
            if (Location == null || Location.Length < 1)
            {
                App.Current.MainPage.DisplayAlert("Alert", "Invalid Location", "OK");
                return false;
            }
            else if (Area <= 0)
            {
                App.Current.MainPage.DisplayAlert("Alert", "Invalid Area", "OK");
                return false;
            }
            return true;
        }
    }
}

