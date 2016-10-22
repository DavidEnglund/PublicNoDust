using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dustbuster.Interfaces;

namespace Dustbuster.DependencyManager
{
    class WebServiceManager
    {
        private IWebServiceConnect MyWebService;

        public WebServiceManager(IWebServiceConnect ThisWebService)
        {
            MyWebService = ThisWebService;
        }
    }
}
