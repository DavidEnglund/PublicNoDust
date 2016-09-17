using Dustbuster.Views;
using Xamarin.Forms;

namespace Dustbuster
{
	public partial class App : Application
	{
		private static DbConnectionManager productsDb;
		private static DbConnectionManager jobsDb;

		public App()
		{
			InitializeComponent();
			InitializeDatabase();

            //Set a style for readingmode to be enabled and disabled
            if (Settings.EnableReadMode)
            {
                Resources["labelStyle"] = Resources["readModeLabelStyle"];  
            }
            else
            {
                Resources["labelStyle"] = Resources["normalLabelStyle"];
            }

			MainPage = new NavigationPage(new DustbusterPage())
			{
				BarBackgroundColor = Color.FromHex("#18b750")
            };
        }

		private async void InitializeDatabase()
		{
			productsDb = new DbConnectionManager("ProductDB.db3");
			jobsDb = new DbConnectionManager("JobDB.db3");

			await productsDb.FillProductTablesAsync();
			jobsDb.CreateJobTable();
		}

		public static DbConnectionManager ProductsDb
		{
			get { return productsDb; }
		}

		public static DbConnectionManager JobsDb
		{
			get { return jobsDb; }
		}

		public static IndustryOptions IndustryOption
		{
			get;
			set;
		}
        #region davids enums set
        public static TrafficOptions TrafficOption
        {
            get;
            set;
        }
        public static DurationOptions DurationOption
        {
            get;
            set;
        }
        public static  WeatherOptions WeatherOption
        {
            get;
            set;
        }
        #endregion
        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public class ListDataViewCell : ViewCell
		{
			public ListDataViewCell()
			{
				var label = new Label()
				{
					//Font = Font.SystemFontOfSize(NamedSize.Default),
					TextColor = Color.Blue
				};
				label.SetBinding(Label.TextProperty, new Binding("TextValue"));
				label.SetBinding(Label.ClassIdProperty, new Binding("DataValue"));
				View = new StackLayout()
				{
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness(12, 8),
					Children = { label }
				};
			}
		}

		public class SimpleObject
		{
			public string TextValue
			{ get; set; }
			public string DataValue
			{ get; set; }
		}
	}

	public enum IndustryOptions { Civil, Mining };
    #region davids enums create
    // added some more enums for all of the users choices to be stored and used - david
    public enum TrafficOptions { TraffickedArea, NonTraffickedArea };
    public enum DurationOptions { UnderAMonth = 14,OverAMonth = 30,OverSixMonths=180};
    public enum WeatherOptions { RainExpected,NoRainExpected};
    #endregion
}
