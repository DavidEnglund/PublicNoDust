using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace Dustbuster
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        internal virtual Task Initialize(params object[] args)
        {
            return Task.FromResult(0);
        }

        protected void OnPropertyChanged(string propertyName)
        {


            //##############################
            //Original code works but has a bug where the Property changed won't get detected on normal implmentation
            //if (PropertyChanged == null) return;

            //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //##############################


            try
            {
                //Correct code here but throws an null reference exception
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch (Exception e)
            {
                if (PropertyChanged == null) return;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Debug.WriteLine(e);
            }


        }


        //Custom method to set property
        protected void SetObservableProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}