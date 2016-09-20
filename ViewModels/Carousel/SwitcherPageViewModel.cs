using System.Linq;
using System.Collections.ObjectModel;

namespace Dustbuster
{
    /// <summary>
    /// Populates the CarouselLayout view with pages containing the recommended products
    /// </summary>

    //view model for carousel view, page indicators, etc.
    public class SwitcherPageViewModel : BaseViewModel
    {
        private int concentration;
        private string productName = "Nice";
        private ObservableCollection<ProductDescription> products;
        ProductDescription currentProduct;

        public SwitcherPageViewModel()
        {
            // The calculation from the accordion will create and send a List of the products to Carousel Page
            // TODO - This is test data, don't forget to delete.
            products = new ObservableCollection<ProductDescription>()
            {
                new ProductDescription(1, 360,"1 App", 125, "Gluon","Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. To achieve all year round protection a high dosage rate will be required. Gluon can be used in combination with seed to achieve even longer lasting results.", "ProductImagePreview.jpg"),
                new ProductDescription(2, 30,"1 App/3 weeks", 150, "DustLig","DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion during weekends. Because it is water soluble re-application will be required after any rain event.", "ProductImagePreview.jpg"),
                new ProductDescription(5, 30,"1 App", 4000, "HydroMulch","HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application and will easily withstand any wind or rain event for up to 3 months. Protection from vehicles and pedestrians is required.", "ProductImagePreview.jpg"),
                
       
             };
            
            CurrentProduct = Products.First();
        }
        

        public ObservableCollection<ProductDescription> Products
        {
            get { return products; }
            set
            {
                SetObservableProperty(ref products, value);

                CurrentProduct = products.FirstOrDefault();
            }
        }

        // When we change the page this is updated by two way databinding
        public ProductDescription CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                SetObservableProperty(ref currentProduct, value);

                if (currentProduct != null)
                {
                    Concentration = currentProduct.Concentration;
                    ProductName = currentProduct.ProductName;
                }
            }
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;

                OnPropertyChanged("ProductName");
            }
        }
        public int Concentration
        {
            get { return concentration; }
            set
            {
                concentration = value;

                OnPropertyChanged("Concentration");
            }
        }
        // TODO - Missing some properties for information that is not in the database or not in the expected format
    }

}