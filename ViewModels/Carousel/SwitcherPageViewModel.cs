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
        private string productName = "Ken sucks big ones";
        private ObservableCollection<ProductDescription> products;
        ProductDescription currentProduct;

        public SwitcherPageViewModel()
        {
            // The calculation from the accordion will create and send a List of the products to Carousel Page
            // TODO - This is test data, don't forget to delete.
            products = new ObservableCollection<ProductDescription>()
            {
                new ProductDescription(1, 360,"1 App", 125, "Gluon","Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. To achieve all year round protection a high dosage rate will be required. Gluon can be used in combination with seed to achieve even longer lasting results.", "Erdal.jpg"),
                new ProductDescription(2, 30,"1 App/3 weeks", 150, "DustLig","DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion during weekends. Because it is water soluble re-application will be required after any rain event.", "Erdal.jpg"),
                new ProductDescription(5, 30,"1 App", 4000, "HydroMulch","HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application and will easily withstand any wind or rain event for up to 3 months. Protection from vehicles and pedestrians is required.", "Erdal.jpg"),
                new ProductDescription(5, 360,"1 App", 4000, "Hydroseeding","HydroSeeding is the best possible option to ensure long term erosion control. Our proprietary blend includes soil enhancers and fast germinating seeds to establish vegetation as soon as possible. Very popular on mine sites and waste treatment plant to complete revegetation requirements. We can add any type of seed or other additives to the product if requested.", "Erdal.jpg"),
                new ProductDescription(3, 30,"3 App/day", 10, "DustJel","DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling.", "Erdal.jpg"),
                new ProductDescription(4, 30,"1 App", 1500, "DustMag","DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required.", "Erdal.jpg"),
                new ProductDescription(4, 180,"1 App/2 months", 1500, "DustMag","DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required.", "Erdal.jpg"),
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