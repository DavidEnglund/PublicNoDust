using System;
using System.Collections.ObjectModel;
using Dustbuster;

namespace Dustbuster
{
    public class DustbusterViewModel
    {
        //private ObservableCollection<ProductResult> products;
        private ObservableCollection<Job> jobs;

        public DustbusterViewModel()
        {
            jobs = new ObservableCollection<Job>();
            
            foreach (Job j in App.JobsDb.GetJobs())
            {
                jobs.Add(j);
            }
        }

        public ObservableCollection<Job> Jobs
        {
            get { return jobs; }
            set { jobs = value; }
        }
    }
}
