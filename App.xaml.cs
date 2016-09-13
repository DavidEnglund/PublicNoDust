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

            /*
            MainPage = new NavigationPage(new DustbusterPage())
            {
                BarBackgroundColor = Color.Blue,
            };
            */
            MainPage = new NavigationPage(new ProductPage())
            {
                BarBackgroundColor = Color.Blue,
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
}

