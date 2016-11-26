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
                    //added clause to show '>100L' for small jobs.
                    if (CalculateQuantity() < 100)
                    {
                        return string.Format("Estimated amount per application: >{0}L", ((float)Math.Ceiling(CalculateQuantity() / 100) * 100).ToString());
                    }
                    else
                    {
                        return string.Format("Estimated amount per application: {0}L", ((float)Math.Ceiling(CalculateQuantity() / 100) * 100).ToString());
                    }
                    
                    
                }
                
            }
        }

        public double Area
        {
            get { return Job.Area; }   //need to see how this is being saved to fix the calc issue
        }

        public string AreaString
        {
            get
            {
                //if job area over 1 sq km (1000000sqm) then it will display area in sq km
                if (Job.Area > 1000000)
                {
                    return string.Format("Job Area: {0:F2}km\xB2", Job.Area/1000000);
                }
                else
                {
                    return string.Format("Job Area: {0}m\xB2", Job.Area.ToString());
                }
            }
        }

		private float CalculateQuantity()
		{
			return (float)(Job.Area * ApplicationRate) / 1000; //mL to L
		}
    }
}
