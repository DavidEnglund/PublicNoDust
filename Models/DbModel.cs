using System.Collections.Generic;

namespace Dustbuster
{
    class DbModel
    {
        public DbModel() { }

        public List<AreaType> AreatypeList { get; set; }
        public List<Product> ProductList { get; set; }
        public List<ProductDescription> ProductDescriptionList { get; set; }
        public List<ProductDuration> ProductDurationList { get; set; }
        public List<ProductMatrix> ProductMatrixList { get; set; }
        public List<Supplier> SupplierList { get; set; }
        public int DbVersion { get; set; }

        public DbModel testModel()
        {
            DbModel testModel = new DbModel();
            testModel.DbVersion = 1337;

            return testModel;
        }
    }
}
