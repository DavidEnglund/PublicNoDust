using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dustbuster
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            GetOnlineHelpLinks();
        }

        private async void GetOnlineHelpLinks()
        {
            HelpService service = new HelpService();
            Links = new ObservableCollection<Link>((await service.GetOnlineHelpLinks()));
        }

        private ObservableCollection<Link> links;
        public ObservableCollection<Link> Links
        {
            get { return links; }
            set
            {
                links = value;

                OnPropertyChanged("Links");
            }
        }


        public string CustomerName
		{
			get
			{
				return Settings.CustomerName;
			}
			set
			{
				Settings.CustomerName = value;

				OnPropertyChanged("CustomerName");
			}
		}

		public int ContactMethod    //Phone (0) or Email (1) from the picker
		{
			get
			{
				return Settings.ContactMethod;
			}
			set
			{
				Settings.ContactMethod = value;

				OnPropertyChanged("ContactMethod");
			}
		}

		public string ContactInfo   //Phone or Email input from the user
		{
			get
			{
				return Settings.ContactInfo;
			}
			set
			{
				Settings.ContactInfo = value;

				OnPropertyChanged("ContactInfo");
			}
		}

		public bool EnableAnalytics
		{
			get
			{
				return Settings.EnableAnalytics;
			}
			set
			{
				Settings.EnableAnalytics = value;

				OnPropertyChanged("EnableAnalytics");
			}
		}

		public bool EnableReadMode
		{
			get
			{
				return Settings.EnableReadMode;
			}
			set
			{
				Settings.EnableReadMode = value;

				OnPropertyChanged("EnableReadMode");
			}

		}

        private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

