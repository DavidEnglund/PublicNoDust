using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Dustbuster
{
    // HomeView Model .... the guy mixes his viewmodels with models
    public class HomeViewModel : BaseViewModel, ITabProvider
    {
       
        public HomeViewModel() {
            Debug.WriteLine("LOG 003 ~ New home model created");
        }

        public string Title { get; set; }
        public Color Background { get; set; }
        public string ImageSource { get; set; }
        public string Blurb { get; set; }



    }
}
