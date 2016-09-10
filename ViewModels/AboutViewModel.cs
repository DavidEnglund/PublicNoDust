using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dustbuster
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public AboutViewModel()
        {
            TapAvatarCommand = new Command((url) =>
            {
                Device.OpenUri(new Uri(url.ToString()));
            });
        }

        public ICommand TapAvatarCommand
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
