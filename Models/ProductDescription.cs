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

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int DurationMaxDays { get; set; }  //aka durationId
		public string ProductName { get; set; }
		public string ProductDesc { get; set; }
		public string ApplicationsRequired { get; set; }
		public int Concentration { get; set; }  //mL per square m

		public float GetQuantity(int area)
		{
			return (float)(area * Concentration) / 1000; //mL to L
		}
	}
}

