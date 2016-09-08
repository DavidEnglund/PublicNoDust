using System;
using SQLite;

namespace Dustbuster
{
	public class ProductMatrix
	{
		public ProductMatrix() { }

		public ProductMatrix(int duration, int areaTypeId, bool willRain, int productId)
		{
			DurationMaxDays = duration;
			AreaTypeId = areaTypeId;
			WillRain = willRain;
			ProductId = productId;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int DurationMaxDays { get; set; }
		public int AreaTypeId { get; set; }
		public bool WillRain { get; set; }
		public int ProductId { get; set; }
	}
}

