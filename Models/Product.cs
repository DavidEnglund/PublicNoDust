using System;
using SQLite;

namespace Dustbuster
{
	public class Product
	{
		public Product() { }

		public Product(int id, string name)
		{
			Id = id;
			ProductName = name;
		}

		[PrimaryKey, Unique]
		public int Id { get; set; }
		public string ProductName { get; set; }
	}
}

