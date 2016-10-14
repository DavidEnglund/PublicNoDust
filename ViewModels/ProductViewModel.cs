﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dustbuster
{
    public class ProductViewModel : INotifyPropertyChanged
    {
		private ObservableCollection<ProductDescription> products;
		private ProductDescription selectedProduct;
		private string applicationsRequired;
		private int applicationRate;
		private float productQuantity;
        double area;

        public ProductViewModel(AccordionViewModel viewModel)
		{
			area = viewModel.Area;
			this.products = new ObservableCollection<ProductDescription>();
			this.selectedProduct = null;
           
		}
        public ProductViewModel(double area)
        {
            this.area = area;
            products = new ObservableCollection<ProductDescription>();
            selectedProduct = null;
        }


        public ObservableCollection<ProductDescription> Products // collected from the database
		{
			get { return products; }
			set { products = value; }
		}
        
		public ProductDescription SelectedProduct
		{
			get { return this.selectedProduct; }
			set
			{
				if (value != null)
				{
					this.selectedProduct = value;

					this.applicationRate = SelectedProduct.Concentration;
					this.applicationsRequired = SelectedProduct.ApplicationsRequired;
					this.productQuantity = CalculateQuantity();

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("SelectedProduct"));
					}
				}
			}
		}

        public string ApplicationsRequired // applications required
		{
			get { return applicationsRequired; }
			set
			{
				applicationsRequired = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ApplicationsRequired"));
				}
			}
		}

        public int ApplicationRate //mL per square m
		{
			get { return applicationRate; }
			set
			{
				applicationRate = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ApplicationRate"));

				}
			}
		}

		public float ProductQuantity //mL per square m
		{
			get { return productQuantity; }
			set
			{
				productQuantity = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ProductQuantity"));

				}
			}
		}

		private float CalculateQuantity()
        {
			return (float)(area * ApplicationRate) / 1000; //mL to L
        }
				   
		public event PropertyChangedEventHandler PropertyChanged;
    }
}
