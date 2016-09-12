using SQLite;
using System;

namespace Dustbuster
{
    public class Job
	{
		public Job() { }

        public Job(int jobType)
        {
            JobType = jobType;
        }

		public Job(int area, int areaTypeId, int durationId, bool willRain, string name)
        {
            Area = area;
            AreaTypeID = areaTypeId;
            DurationMaxDays = durationId;
			WillRain = willRain;
            Description = name;
        }

        [PrimaryKey, AutoIncrement]
        public int JobID { get; set; }
        public int JobType { get; set; }  //1 = civil, 2 = mining
        public int Area { get; set; }
        public int AreaTypeID { get; set; }
        public int DurationMaxDays { get; set; }
        public bool WillRain { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}