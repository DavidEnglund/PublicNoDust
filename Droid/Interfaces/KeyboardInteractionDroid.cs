using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using Xamarin.Forms;
using Dustbuster.Droid;

[assembly: Dependency(typeof(KeyboardInteractionDroid))]
namespace Dustbuster.Droid
{
    /**
     * This class is implementation of IKeyboardInteraction in xamarin project.
     * this class aims to hide soft-keyboard
     * Just simply use this lines
     * "IKeyboardInteractions keyboardInteractions = DependencyService.Get<IKeyboardInteractions>();"
     * "keyboardInteractions.HideKeyboard ();"
     *
     **/
    public class KeyboardInteractionDroid : IKeyboardInteraction
    {
        public void HideKeyboard()
        {
            var inputMethodManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && Xamarin.Forms.Forms.Context is Activity)
            {
                var activity = Xamarin.Forms.Forms.Context as Activity;
                var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, 0);
            }
        }
    }
}