using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Dustbuster.ViewModels
{
    public class ProductCalculationViewModel
    {

        public ObservableCollection <ProductDescription> ProductSource { get; set; }// collection from the database
        public int ApplicationsRequired { get; set; } // applications required
        public int ApplicationRate { get; set; }  //mL per square m
        public float ProductArea { get; set; }// bound to area calculations

        public float TotalProductQty()
        {
            return (float)(ProductArea * ApplicationRate * ApplicationsRequired) / 1000; //mL to L
        }
    }
}
