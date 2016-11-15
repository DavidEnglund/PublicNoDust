using System;

namespace Dustbuster
{
    public class ProductResult
    {
		public ProductDescription Description { get; set; }
		public Job Job { get; set; }

		public string ProductName
		{
			get { return Description.ProductName; }
		}

		public string ProductDesc
		{
			get { return Description.ProductDesc; }
		}

		public string ImageSource
		{
			get { return Description.ImageSource; }
		}

		public string ApplicationsRequired // applications required
		{
			get { return Description.ApplicationsRequired; }
		}

		public int ApplicationRate //mL per square m
		{
			get { return Description.Concentration; }
		}

		public float ProductQuantity //mL per square m
		{
            get { return (float)Math.Ceiling(CalculateQuantity() / 100) * 100; }
        }

        public double Area
        {
            get { return Job.Area; }
        }

		private float CalculateQuantity()
		{
			return (float)(Job.Area * ApplicationRate) / 1000; //mL to L
		}
    }
}
