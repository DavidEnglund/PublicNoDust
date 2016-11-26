using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Dustbuster
{
    public class DustbusterViewModel
    {
        //private ObservableCollection<ProductResult> products;
        private ObservableCollection<Job> jobs;

        public DustbusterViewModel()
        {
            jobs = new ObservableCollection<Job>();
            List<Job> DBjobs = App.JobsDb.GetJobs().ToList();
            int count = DBjobs.Count;

            if (count > 5)
            {
                for (int i = count - 5; i < count; i++)
                {
                    jobs.Add(DBjobs[i]);
                }
            }
            else
            {
                foreach (Job j in App.JobsDb.GetJobs())
                {
                    jobs.Add(j);
                }
            }
        }

        public ObservableCollection<Job> Jobs
        {
            get { return jobs; }
            set { jobs = value; }
        }
    }
}
