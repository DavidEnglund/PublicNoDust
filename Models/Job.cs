using SQLite;
using System;
using System.Collections.ObjectModel;

namespace Dustbuster
{
    public class Job
	{
		public Job() { }
        public Job(int jobType)
        {
            JobType = jobType;
        }
		public Job(int jobType, int area, int areaTypeId, int durationMaxDays, bool willRain, DateTime creationDate, string location)
        {
            JobType = jobType;
            Area = area;
            AreaTypeID = areaTypeId;
            DurationMaxDays = durationMaxDays;
			WillRain = willRain;
            CreationDate = creationDate;
            Location = location;
        }


        [PrimaryKey, AutoIncrement]
        public int JobID { get; set; }
        public int JobType { get; set; }  //0 = civil, 1 = mining
        public double Area { get; set; }
        public int AreaTypeID { get; set; }
        public int DurationMaxDays { get; set; }
        public bool WillRain { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
    }
}