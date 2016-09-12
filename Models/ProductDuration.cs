using System;
namespace Dustbuster
{
	public class ProductDuration
	{
		public ProductDuration() { }

		public ProductDuration(int days, string text)
		{
			MaxDays = days;
			Text = text;
		}

		public int MaxDays { get; set; }
		public string Text { get; set; }

	}
}

