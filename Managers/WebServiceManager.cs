using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    public class WebServiceManager
    {
        private IWebServiceConnect webService;

        public WebServiceManager(IWebServiceConnect webService)
        {
            this.webService = webService;
        }
    }
}
