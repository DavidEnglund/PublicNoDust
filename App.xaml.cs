using Dustbuster.Views;
using Xamarin.Forms;
using System.Diagnostics;

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

			MainPage = new NavigationPage(new DustbusterPage())
			{
				BarBackgroundColor = Color.FromHex("#18b750")
			};
		}

        private async void InitializeDatabase()
        {
            productsDb = new DbConnectionManager("ProductDB.db3");
            jobsDb = new DbConnectionManager("JobDB.db3");

            //productsDb.FillProductTablesAsync();
            Debug.WriteLine("DATABASE DEBUG: Job tables pre Job table check: {0}", jobsDb.GetTableInfo("Job"));
            if (jobsDb.GetTableInfo("Job") == 0)
            {
                jobsDb.CreateJobTable();
            }
            Debug.WriteLine("DATABASE DEBUG: Job tables post Job table check: {0}", jobsDb.GetTableInfo("Job"));

            // updating database from online if there is an updated version available.
            if (await DataAccess.OnlineUpdater.UpdateAvailable())
            {
                await DataAccess.OnlineUpdater.UpdateProductMatrix();
            }
            Debug.WriteLine("--== database updated ==--");
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
		public static WeatherOptions WeatherOption
		{
			get;
			set;
		}
		#endregion

        //changed method to async to allow database update R.L
		protected async override void OnStart()
		{
			// Handle when your app starts
			// this will start the calendar access service for iOS - for android it currently does nothing
			// DependencyService.Get<IReminderService>(DependencyFetchTarget.GlobalInstance).CreateService();
            // Starts updating the product database. R.L
            //await DependencyService.Get<IWebServiceConnect>().GetDBVersion();
            //var result = await DependencyService.Get<IWebServiceConnect>().GetDB();
        }

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}

	public enum IndustryOptions { Civil, Mining, None };
	#region davids enums create
	// added some more enums for all of the users choices to be stored and used - david
	public enum TrafficOptions { NonTraffickedArea, TraffickedArea, None };
	public enum DurationOptions { Under1Month=30, Over1Month=90, Under6Months=180, Over6Months=360, None };
	public enum WeatherOptions { RainExpected, NoRainExpected, None };
	#endregion
}
