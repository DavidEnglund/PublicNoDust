using System;
using SQLite;

namespace Dustbuster
{
	public class AreaType
	{
		public AreaType() { }

		[PrimaryKey, Unique]
		public int ID { get; set; }
		public string Text { get; set; }
	}
}

