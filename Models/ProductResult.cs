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

        public string ApplicationRateString //mL per square m
        {
            get
            {
                if (Description.ProductId == 5)
                {
                    return "";
                }
                else
                {
                    return string.Format("Application rate: {0}mL/m\xB2", Description.Concentration);
                    //return Description.Concentration;
                }
                
            }
        }

        public float ProductQuantity //mL per square m
		{
            get { return (float)Math.Ceiling(CalculateQuantity() / 100) * 100; }
        }

        public string ProductQuantityString //mL per square m
        {
            get
            {
                if (Description.ProductId == 5)
                {
                    return "";
                }
                else
                {
                    //return (float)Math.Ceiling(CalculateQuantity() / 100) * 100;
                    return string.Format("Estimated amount per application: {0}L", ((float)Math.Ceiling(CalculateQuantity() / 100) * 100).ToString());
                }
                
            }
        }

        public double Area
        {
            get { return Job.Area; }   //need to see how this is being saved to fix the calc issue
        }

		private float CalculateQuantity()
		{
			return (float)(Job.Area * ApplicationRate) / 1000; //mL to L
		}
    }
}
