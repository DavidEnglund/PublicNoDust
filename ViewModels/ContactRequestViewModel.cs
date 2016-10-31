using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dustbuster
{
    public class ContactRequestViewModel : INotifyPropertyChanged
    {
        private DateTime selectedDate;
        private ICommand datePickerCommand;

        public ContactRequestViewModel(ContactRequestPage page)
        {
            selectedDate = DateTime.Now;

            datePickerCommand = new Command(() =>
            {
         //       page.DatePicker.Focus();
            });
            
        }

        public ICommand DatePickerCommand
        {
            get { return this.datePickerCommand; }
        }

        public DateTime SelectedDate
        {
            get { return this.selectedDate; }
            set 
			{
                this.selectedDate = value;
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("SelectedDate"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
