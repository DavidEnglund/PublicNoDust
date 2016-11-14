using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    public class WebServiceResponseMsg
    {
        public WebServiceErrorMsg[] errors { get; set; }
        public Boolean success { get; set; }
    }
}
