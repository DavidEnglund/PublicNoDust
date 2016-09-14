using Xamarin.Forms;
using System.Diagnostics;

namespace Dustbuster
{
    public class PageModel : BaseViewModel, ITabProvider
    {
        public PageModel() {
            Debug.WriteLine("LOG 003 ~ New home model created");
        }

        public string Title { get; set; }
        public Color Background { get; set; }
        public string ImageSource { get; set; }
        public string Blurb { get; set; }
    }
}
