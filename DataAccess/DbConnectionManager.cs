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
        private static object locker = new object(); 

        public DbConnectionManager(string fileName)
        {
            dbConnection = DependencyService.Get<ISQLite>().GetConnection(fileName);
        }

        public SQLiteConnection DbConnection
        {
            get { return this.dbConnection; }
        }

        public int GetTableInfo(string tabletype)
        {
            return dbConnection.GetTableInfo(tabletype).Count;
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
            //couldnt get pragma user_version to work from code
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

        //heres a manual overload that can be run without a job object
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

        // here are all the methods to be run to update the database from online.
        // they will need to be given a List<> of the table object that is to be updated.
        #region update methods


        public int UpdateProductMatrix(List<ProductMatrix> fromOnline)
        {
            dbConnection.DropTable<ProductMatrix>();
            dbConnection.CreateTable<ProductMatrix>();
            int rowsAltered = 0;
<<<<<<< HEAD
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        public int UpdateProduct(List<Product> fromOnline)
        {
            dbConnection.DropTable<Product>();
            dbConnection.CreateTable<Product>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        public int UpdateProductDescription(List<ProductDescription> fromOnline)
        {
            dbConnection.DropTable<ProductDescription>();
            dbConnection.CreateTable<ProductDescription>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        public int UpdateProductDuration(List<ProductDuration> fromOnline)
        {
            dbConnection.DropTable<ProductDuration>();
            dbConnection.CreateTable<ProductDuration>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
=======
            lock (locker)
            {
                foreach (var matrix in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(matrix);
                }
            }
>>>>>>> d211036382a4edad57773ef2fd2e2ff5f3c1b653
            return rowsAltered;
        }

        public int UpdateDBMetaData(List<DBMetaData> fromOnline)
        {
            dbConnection.DropTable<DBMetaData>();
            dbConnection.CreateTable<DBMetaData>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        public int UpdateAreaType(List<AreaType> fromOnline)
        {
            dbConnection.DropTable<AreaType>();
            dbConnection.CreateTable<AreaType>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        public int UpdateSupplier(List<Supplier> fromOnline)
        {
            dbConnection.DropTable<Supplier>();
            dbConnection.CreateTable<Supplier>();
            int rowsAltered = 0;
            lock (locker)
            {
                foreach (var row in fromOnline)
                {
                    rowsAltered += dbConnection.Insert(row);
                }
            }
            return rowsAltered;
        }

        #endregion
    }
}
