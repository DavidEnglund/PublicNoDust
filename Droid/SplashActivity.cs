using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace Dustbuster.Droid
{
    [Activity(Label = "Dustbuster.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
		}

		protected override void OnResume()
		{
			base.OnResume();

			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}
