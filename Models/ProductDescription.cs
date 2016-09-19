using System;
using SQLite;

namespace Dustbuster
{
	public class ProductDescription
	{
		public ProductDescription() { }



        public ProductDescription(int productId, int duration, string applications, int concentration, string name, string description)
        {
            ProductId = productId;
            DurationMaxDays = duration;
            ProductName = name;
            ProductDesc = description;
            ApplicationsRequired = applications;
            Concentration = concentration;
         
        }

        public ProductDescription(int productId, int duration, string applications, int concentration, string name, string description, string imageSource)
		{
			ProductId = productId;
			DurationMaxDays = duration;
			ProductName = name;
			ProductDesc = description;
			ApplicationsRequired = applications;
			Concentration = concentration;
            ImageSource = imageSource;
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int DurationMaxDays { get; set; }  //aka durationId
		public string ProductName { get; set; }
		public string ProductDesc { get; set; }
		public string ApplicationsRequired { get; set; }
		public int Concentration { get; set; }  //mL per square m
        public string ImageSource { get; set; } // summon the Erdal

		public float GetQuantity(int area)
		{
			return (float)(area * Concentration) / 1000; //mL to L
		}
	}
}

