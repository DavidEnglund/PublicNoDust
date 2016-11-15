using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dustbuster.Tests
{
    public class CalculationTests
    {
        private AccordionViewModel viewModel;

        public CalculationTests()
        {
            viewModel = new AccordionViewModel(null);
        }
        // Test method for area 
        [Theory]
        [InlineData(10, 20, 200)]
        public void CalculateAreaCommand(int width, int length, int areaExpected)
        {
            viewModel.Width = width;
            viewModel.Length = length;

            viewModel.Calculate.Execute(null);

            Assert.Equal(areaExpected, viewModel.Area);
        }
        // Test method for changing Length unit with metres and km
        [Theory]
        [InlineData(10, 1000, 1)]
        public void CalculateLengthCommand( int lengthUnit,int metre, int lengthExpected)
        {
            viewModel.Length = metre;

            viewModel.ChangeLengthUnit.Execute(null);

            Assert.Equal(lengthExpected, viewModel.Length);
        }

        // Test method for changing Width unit with metres and km
        [Theory]
        [InlineData(10, 1000, 1)]
        public void CalculateWidthCommand(int widthUnit, int metre, int widthExpected)
        {
            viewModel.Width = metre;

            viewModel.ChangeWidthUnit.Execute(null);

            Assert.Equal(widthExpected, viewModel.Width);
        }

        // Testing Method for application rate -X
        [Theory]
        [InlineData(35)]
        public void ProductApplicationRate(int rateExpected)
        {
            ProductResult productResult = new ProductResult();
            productResult.Description = new ProductDescription(1, 30, "1 App", 35, "Gluon", "Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A light dosage rate will give you enough protection from the elements for up to a month.");
            productResult.Job = new Job(0, 25000, 1, 90, true, DateTime.Now, "second Job object");

            Assert.Equal(rateExpected, productResult.ApplicationRate);
            
        }

        // Testing Method for application -Y
        [Theory]
        [InlineData("1 App")]
        public void ProductApplications(string applicationsExpected)
        {
            ProductResult productResult = new ProductResult();
            productResult.Description = new ProductDescription(1, 30, "1 App", 35, "Gluon", "Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A light dosage rate will give you enough protection from the elements for up to a month.");
            productResult.Job = new Job(0, 25000, 1, 90, true, DateTime.Now, "second Job object");

            Assert.Equal(applicationsExpected, productResult.ApplicationsRequired);

        }

        // Testing Method for how many applications -Z 
        [Theory]
        [InlineData(10, 100)]
        public void ProductQuantity(int area, int qtyExpected)
        {
            ProductResult productResult = new ProductResult();
            productResult.Description = new ProductDescription(1, 30, "1 App", 35, "Gluon", "Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a “veneer” or “crust” which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A light dosage rate will give you enough protection from the elements for up to a month.");
            productResult.Job = new Job(0, area, 1, 90, true, DateTime.Now, "second Job object");

            Assert.Equal(qtyExpected, productResult.ProductQuantity);

        }
    }
}
