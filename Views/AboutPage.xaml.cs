﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dustbuster
{
    public partial class AboutPage : ContentPage
    {

        public AboutPage()
        {
            this.BindingContext = new AboutViewModel();

            InitializeComponent();
        }
    }
}
