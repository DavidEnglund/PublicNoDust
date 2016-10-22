using Xamarin.Forms;
using Dustbuster.iOS;

[assembly: Dependency(typeof(KeyboardInteractionIOS))]
namespace Dustbuster.iOS
{
    class KeyboardInteractionIOS : IKeyboardInteraction
    {
        public void HideKeyboard()
        {
            //throw new NotImplementedException();
        }
    }
}