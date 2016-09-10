using System;
using System.Collections.ObjectModel;
using Dustbuster;

namespace Dustbuster.ViewModels
{
    public class DustbusterViewModel
    {
        private ObservableCollection<ProductResult> products;

        public DustbusterViewModel()
        {
            products = new ObservableCollection<ProductResult>();

            products.Add(new ProductResult() { ProductName = "Gluon", DateCreated = DateTime.Now, Amount = "85000 L" });
            products.Add(new ProductResult() { ProductName = "DustLig", DateCreated = DateTime.Now, Amount = "120 L" });
            products.Add(new ProductResult() { ProductName = "Gluon26", DateCreated = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", DateCreated = DateTime.Now, Amount = "180 L" });
            products.Add(new ProductResult() { ProductName = "DustJel", DateCreated = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", DateCreated = DateTime.Now, Amount = "160 L" });
            products.Add(new ProductResult() { ProductName = "DustJel", DateCreated = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "Gluon", DateCreated = DateTime.Now, Amount = "85 L" });
            products.Add(new ProductResult() { ProductName = "DustLig", DateCreated = DateTime.Now, Amount = "177 L" });
        }

        public ObservableCollection<ProductResult> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
