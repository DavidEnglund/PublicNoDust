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
            
                       
            Pages = new List<HomeViewModel>() {
                new HomeViewModel { Title = "1", Background = Color.White, ImageSource = "icon.png", Blurb = "blah1"},
                new HomeViewModel { Title = "2", Background = Color.Red, ImageSource = "icon.png", Blurb = "blah2"},
                new HomeViewModel { Title = "3", Background = Color.Blue, ImageSource = "icon.png", Blurb = "blah3" },
                new HomeViewModel { Title = "4", Background = Color.Yellow, ImageSource = "icon.png", Blurb = "blah4" },
            };

            CurrentPage = Pages.First();
            CurrentTitle = Pages.First().Title;
        }

        IEnumerable<HomeViewModel> _pages;
        HomeViewModel _currentPage;
        String _currentTitle;


        // get set shit
        public IEnumerable<HomeViewModel> Pages
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

        public HomeViewModel CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                SetObservableProperty(ref _currentPage, value);
                CurrentTitle = value.Title;
            }
        }

        //CurrentTitle Property
        public String CurrentTitle
        {
            get
            {
                return _currentTitle;
            }
            set
            {
                SetObservableProperty(ref _currentTitle, value);
                //_currentTitle = value;
                //OnPropertyChanged("CurrentTitle");
            }
        }
    }

}