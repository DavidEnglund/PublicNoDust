using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System;

namespace Dustbuster
{
    public class DbConnectionManager
    {
		private SQLiteConnection dbConnection;
        private static object locker = new object(); //functions as a using block and prevents parallel access

        #region data_structures

        private string[] AreaTypeArray = new string[] { "Open Area", "Road" };  //open area = 0, roads = 1

        //string[] DurationArray = new string[] {"Short term", "Medium term", "Long term"};
		private List<ProductDuration> durationList = new List<ProductDuration>
        {
            new ProductDuration(30,"Short Term"),
            new ProductDuration(90,"Medium To Long Term"),        //applies to roads only
            new ProductDuration(180,"Medium Term"),
            new ProductDuration(360,"Long Term")
        };

		private List<Supplier> supplierList = new List<Supplier>
        {
            new Supplier(1,"Sunhawk","0894592785","wa@sunhawk.com.au"),
            new Supplier(2,"Rainstorm WA","0894520235","info@rainstorm.com.au"),
            new Supplier(3,"Rainstorm Victoria","0352824024","info@rainstorm.com.au")
        };

        //a pre-saved job for testing   TODO - Remove later
        private Job tempJob = new Job(1, 1338, 1, 30, false, DateTime.Now, "tempJob object");

        //now that each area/duration combo has its own description (and different names for hydromulch),
        //this is pretty much useless, mebbe use for pictures or something
		private List<Product> productList = new List<Product> {
            new Product(1, "Gluon"),
            new Product(2, "DustLig"),
            new Product(3, "DustJel"),
            new Product(4, "DustMag"),
            new Product(5, "Hydromulch")
        };

		private List<ProductDescription> productDescriptions = new List<ProductDescription>
        {
            new ProductDescription(1, 30,"1 Application only", 35, "Gluon","Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A light dosage rate will give you enough protection from the elements for up to a month."),
            new ProductDescription(1, 180,"1 Application only", 75, "Gluon","Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A medium dosage rate will give your site protection for up to 6 months."),
            new ProductDescription(1, 360,"1 Application only", 125, "Gluon","Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. To achieve all year round protection a high dosage rate will be required. Gluon can be used in combination with seed to achieve even longer lasting results."),
            new ProductDescription(2, 30,"1 Application/3 weeks", 150, "DustLig","DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion during weekends. Because it is water soluble re-application will be required after any rain event."),
            new ProductDescription(2, 180,"1 Application/3 weeks", 150, "DustLig","DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion. Because it is water soluble re-application will be required after any rain event. Without rain it is suggested to re-apply every 2 to 3 weeks for optimal protection."),
            //explanation sheet entry 'DUSTLIG – TRAFFICED AREAS – SHORT TERM' is not on matrix (p1).
            /*
             //unused text from desc sheet:
                 DUSTLIG – TRAFFICED AREAS – SHORT TERM: DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression used to achieve protection from wind erosion. For optimal protection on roads, regular re-application is needed to ensure build up in the road surface.
            */
            new ProductDescription(5, 30,"1 Application only", 4000, "HydroMulch","HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application and will easily withstand any wind or rain event for up to 3 months. Protection from vehicles and pedestrians is required."),
            new ProductDescription(5, 180,"1 Application only", 4000, "HydroMulch with Gluon","HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application. Mixed with Gluon will ensure extra strength and durability on the ground for up to 6 months. Protection from vehicles and pedestrians is required."),
            new ProductDescription(5, 360,"1 Application only", 4000, "Hydroseeding","HydroSeeding is the best possible option to ensure long term erosion control. Our proprietary blend includes soil enhancers and fast germinating seeds to establish vegetation as soon as possible. Very popular on mine sites and waste treatment plant to complete revegetation requirements. We can add any type of seed or other additives to the product if requested."),

            new ProductDescription(3, 30,"3 Applications/day", 10, "DustJel","DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling."),
            new ProductDescription(3, 90,"3 Applications/day", 10, "DustJel","DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling."),
            
            new ProductDescription(4, 30,"1 Application only", 1500, "DustMag","DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required."),
            new ProductDescription(4, 90,"1 Application/2 months", 1500, "DustMag","DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required."),

            //medium and long term road solutions were folded into one result
            //new ProductDescription(3, 360,"3 App/day", 10, "DustJel","DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling."),
            //new ProductDescription(4, 360,"1 App/2 months", 1500, "DustMag","DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required.")

        };

        List<ProductMatrix> ProductMatrixList = new List<ProductMatrix>
        {
            /*
             IMPORTANT: for the time being i have assumed the following periodic application rates:

             id 2 - DustLig - 1 app/3 weeks
             id 4 - DustMag - 1 app/2 months

             NOTES:
             - product sheet 2 says dustLig (product_ID 2) should be applied 3 times a week for short term jobs 
                and once every 3 weeks for long term jobs (so perhaps clarify application property)
             - short term road is two lines on p1 but one selection on p2, need to clarify (mebbe one is wet, one is dry?)
               (specifically it says short term roads should get dustjel and dustlig, which would cover both rain and no rain options
               for now assuming it means one option for either)
             */

            //OPEN AREAS
            //short term
            new ProductMatrix(30,0,false,1),
            new ProductMatrix(30,0,true,1),
            new ProductMatrix(30,0,false,2),
            new ProductMatrix(30,0,false,5), 
            new ProductMatrix(30,0,true,5),
            //medium term
            new ProductMatrix(180,0,false,1),
            new ProductMatrix(180,0,true,1),
            new ProductMatrix(180,0,false,2),
            new ProductMatrix(180,0,false,5),
            new ProductMatrix(180,0,true,5),
            //long term
            new ProductMatrix(360,0,false,1),
            new ProductMatrix(360,0,false,5),

            //ROADS
            //short term
            new ProductMatrix(30,1,true,4), 
            new ProductMatrix(30,1,false,3),  
            //medium term
            new ProductMatrix(90,1,false,4),
            new ProductMatrix(90,1,true,3)
            //long term (folded into medium term)
            //new ProductMatrix(360,1,false,4),
            //new ProductMatrix(360,1,true,3)

        };


        #endregion

        public DbConnectionManager(string fileName)
        {
            dbConnection = DependencyService.Get<ISQLite>().GetConnection(fileName);
        }

        public DbConnectionManager()
        {
            dbConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

		public SQLiteConnection DbConnection
		{
			get { return this.dbConnection; }
		}
        
        //data retrieval methods
        #region JobsDbAccessMethods
        public IEnumerable<Job> GetJobs()
        {
            lock (locker)

            {
               return dbConnection.Query<Job>("SELECT * FROM [Job]").ToList();
            }
        }

        public int SaveJob(Job job)
        {
            return dbConnection.Insert(job);
        }

        public int DeleteJob(Job job)
        {
            return dbConnection.Delete(job);
        }

        public UserInfo GetUserInfo()
        {
            int key = 1;
            return dbConnection.Get<UserInfo>(key);
        }

        public void UpdateUserInfo(UserInfo user)
        {
            //user.ID must be 1
            lock (locker)
            {
                dbConnection.Update(user);
            }
        }
        #endregion

        #region ProductDbAccessMethods

        public int GetDBVersion()
        {
            //couldnt get pragma user_version to work
            int key = 1;
            lock (locker)
            {
                return dbConnection.Get<DBMetaData>(key).DBVersion;
            }
        }

        public IEnumerable<ProductDuration> GetDurationList()
        {
            lock (locker)
            {
                return (from i in dbConnection.Table<ProductDuration>() select i).ToList();
            }
        }

        public IEnumerable<AreaType> GetAreaTypeList()
        {
            lock (locker)
            {
                return (from i in dbConnection.Table<AreaType>() select i).ToList();
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            lock (locker)
            {
                return (from i in dbConnection.Table<Product>() select i).ToList();
            }
        }

        //should parameterise this
        public IEnumerable<ProductMatrix> SelectProducts(Job job)
        {
            int rainToInt = 0;
            if (job.WillRain)
            {
                rainToInt = 1;
            }

            lock (locker)
            {
				return dbConnection.Query<ProductMatrix>("SELECT * FROM [ProductMatrix] " +
                                                         $"WHERE [DurationMaxDays] = {job.DurationMaxDays} " +
				                                         $"AND [AreaTypeId] = {job.AreaTypeID} AND [WillRain] = {rainToInt}").ToList();
            }
        }

        //heres a manual overload, that can be run without a job object
        public IEnumerable<ProductMatrix> SelectProducts(int maxDays, int areaType, bool rain)
        {
            //sqlite bools are ints (0 or 1 [or non-zero])
            int rainToInt = 0;
            if (rain)
            {
                rainToInt = 1;
            }

            lock (locker)
            {
				return dbConnection.Query<ProductMatrix>("SELECT * FROM [ProductMatrix] " +
				                                         $"WHERE [DurationMaxDays] = {maxDays} " +
														 $"AND [AreaTypeId] = {areaType} AND [WillRain] = {rainToInt}").ToList();
            }
        }

        //could hardcode the ids for these and insert them straight into matrix, save a step
        public ProductDescription GetProductInfo(int pId, int duration)
        {
            lock (locker)
            {
                return dbConnection.Table<ProductDescription>().Where(c => c.ProductId == pId && c.DurationMaxDays == duration).FirstOrDefault();
            }
        }

        public ProductDescription GetProductInfo(ProductMatrix matrix)
        {
            lock (locker)
            {
                return dbConnection.Table<ProductDescription>().Where(c => c.ProductId == matrix.ProductId && c.DurationMaxDays == matrix.DurationMaxDays).FirstOrDefault();
            }
        }

        public Product GetProduct(int productId)
        {
            lock (locker)
            {
                return dbConnection.Get<Product>(productId);
            }
        }


        public AreaType GetAreaTypeInfo(int areaId)
        {
            lock (locker)
            {
                return dbConnection.Get<AreaType>(areaId);
            }
        }

        public ProductDuration GetDurationInfo(int durationId)
        {
            lock (locker)
            {
                return dbConnection.Get<ProductDuration>(durationId);
            }
        }

        public Supplier GetCompanyInfo(int companyId)
        {
            lock (locker)
            {
                return dbConnection.Get<Supplier>(companyId);
            }
        }

        //for testing
        public IEnumerable<ProductMatrix> SelectAllProducts()
        {

            lock (locker)
            {
                return dbConnection.Query<ProductMatrix>("SELECT * FROM [ProductMatrix]").ToList();
            }
        }

        #endregion
        //the following methods build the DB locally and may need to be edited once the external db is deployed
        #region BuildAndPopulateDbLocally

        //without doing this async the first page hangs for a few seconds (before rendering anything) while loading, 
        //also allows a progress icon to be used

        public async Task FillProductTablesAsync()
        {

            Task task0 = Task.Run(() => CreateProductTables());
            await Task.WhenAll(task0);

            Task task1 = Task.Run(() => FillDurationTable());
            Task task2 = Task.Run(() => FillAreaTypeTable());
            Task task3 = Task.Run(() => FillProductTable());  //use hashtable or something
            Task task4 = Task.Run(() => FillProductMatrixTable());
            Task task5 = Task.Run(() => FillSupplierTable());
            Task task6 = Task.Run(() => FillProductDescriptionTable());
            Task task7 = Task.Run(() => setDBVersion());

            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);
        }

        public void CreateProductTables()
        {
            dbConnection.CreateTable<Product>();
            dbConnection.CreateTable<AreaType>();
            dbConnection.CreateTable<ProductDuration>();
            dbConnection.CreateTable<ProductMatrix>();
            dbConnection.CreateTable<Supplier>();
            dbConnection.CreateTable<ProductDescription>();
            dbConnection.CreateTable<DBMetaData>();
        }

        public void CreateJobTable()
        {
            dbConnection.CreateTable<Job>();
            dbConnection.CreateTable<UserInfo>();
            //dbConnection.Insert(tempJob); //a demo job for debug
            //dbConnection.Insert(new Job(0, 25000, 1, 90, true, DateTime.Now, "second Job object"));
        }


        public void setDBVersion()
        {
            DBMetaData sigh = new DBMetaData(4);
            lock (locker)
            {
                dbConnection.Insert(sigh);
            }
        }

        //set to return no. of altered rows, reset to void later if nec
        //decided to set ids here rather than autoincrement to match picker, also to match other inputs
        public int FillDurationTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var s in durationList)
                {
                    ProductDuration duration = new ProductDuration();
                    duration.MaxDays = s.MaxDays;
                    duration.Text = s.Text;

                    rowsAltered += dbConnection.Insert(duration);
                }
            }
            return rowsAltered;
        }

        public int FillAreaTypeTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var area in AreaTypeArray)
                {
                    AreaType areaType = new AreaType();
                    areaType.Text = area;
                    areaType.ID = rowsAltered;
                    rowsAltered += dbConnection.Insert(areaType);

                }
            }
            return rowsAltered;
        }

        public int FillProductTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var product in productList)
                {
                    rowsAltered += dbConnection.Insert(product);
                }
            }
            return rowsAltered;
        }

        public int FillProductMatrixTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var matrix in ProductMatrixList)
                {
                    rowsAltered += dbConnection.Insert(matrix);
                }
            }
            return rowsAltered;
        }

        public int FillProductDescriptionTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var description in productDescriptions)
                {
                    rowsAltered += dbConnection.Insert(description);
                }
            }
            return rowsAltered;
        }

        public int FillSupplierTable()
        {
            int rowsAltered = 0;

            lock (locker)
            {
                foreach (var supplier in supplierList)
                {
                    rowsAltered += dbConnection.Insert(supplier);
                }
            }
            return rowsAltered;
        }

        #endregion
    }
}
