using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster
{
    public partial class CalculationPage : ContentPage
    {
        public CalculationPage()
        {
            BindingContext = new AreaViewModel();
            InitializeComponent();
        }
    }
}
