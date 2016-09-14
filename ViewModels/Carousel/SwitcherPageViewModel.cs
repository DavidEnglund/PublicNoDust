using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Dustbuster
{
    /// <summary>
    /// Populates the CarouselLayout view with pages to scroll between.
    /// </summary>

    //view model for carousel view, page indicators, etc.
    public class SwitcherPageViewModel : BaseViewModel
    {
        public SwitcherPageViewModel()
        {
            Debug.WriteLine("LOG 002 ~ SwitcherPageViewModel called");


            // The calculation from the accordian will create and send a List of the products to Carousel Page
            // The Carousel Page labels info and image will update accordingly
            //This is where we fill a list of information we will use to build the carousel pages.
            //The following list is for testing functionality only, and will be removed once merged
            Pages = new List<PageModel>() {
                new PageModel { Title = "1", Background = Color.White, ImageSource = "icon.png", Blurb = "blah1"},
                new PageModel { Title = "2", Background = Color.Red, ImageSource = "icon.png", Blurb = "blah2"},
                new PageModel { Title = "3", Background = Color.Blue, ImageSource = "icon.png", Blurb = "blah3" },
                new PageModel { Title = "4", Background = Color.Yellow, ImageSource = "icon.png", Blurb = "blah4" },
            };

            CurrentPage = Pages.First();
        }

        IEnumerable<PageModel> _pages;
        PageModel _currentPage;

        public IEnumerable<PageModel> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                SetObservableProperty(ref _pages, value);
                CurrentPage = Pages.FirstOrDefault();
            }
        }

        public PageModel CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                SetObservableProperty(ref _currentPage, value);
            }
        }
    }

}