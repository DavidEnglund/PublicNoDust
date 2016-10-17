using Xamarin.Forms;
using Dustbuster.iOS.Interfaces;
using Dustbuster.Interfaces;

[assembly: Dependency(typeof(KeyboardInteractionIOS))]
namespace Dustbuster.iOS.Interfaces
{
    class KeyboardInteractionIOS : IKeyboardInteraction
    {
        public void HideKeyboard()
        {
            //throw new NotImplementedException();
        }
    }
}