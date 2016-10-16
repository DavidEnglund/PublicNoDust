using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dustbuster
{
    public class ProductViewModel : INotifyPropertyChanged
    {
		private ObservableCollection<ProductResult> products;
		private ProductResult selectedProduct;
		private string applicationsRequired;
		private int applicationRate;
		private float productQuantity;
        double area;

        public ProductViewModel(AccordionViewModel viewModel)
		{
			area = viewModel.Area;
			this.products = new ObservableCollection<ProductResult>();
			this.selectedProduct = null;
           
		}
        public ProductViewModel(double area)
        {
            this.area = area;
            products = new ObservableCollection<ProductResult>();
            selectedProduct = null;
        }


        public ObservableCollection<ProductResult> Products // collected from the database
		{
			get { return products; }
			set { products = value; }
		}
        
		public ProductResult SelectedProduct
		{
			get { return this.selectedProduct; }
			set
			{
				if (value != null)
				{
					this.selectedProduct = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("SelectedProduct"));
					}
				}
			}
		}
				   
		public event PropertyChangedEventHandler PropertyChanged;
    }
}
