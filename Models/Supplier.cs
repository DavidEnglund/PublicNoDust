using System;
using SQLite;

namespace Dustbuster
{
	public class Supplier
	{
		public Supplier() { }

		public Supplier(int id, string name, string phone, string email)
		{
			Id = id;
			Name = name;
			ContactPhone = phone;
			ContactEmail = email;
		}

		[PrimaryKey, Unique]
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContactPhone { get; set; }
		public string ContactEmail { get; set; }
	}
}

