using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster.Models
{
    class UserInfo
    {
        public int SPId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool InfoChanged { get; set; }

        public UserInfo()
        {

        }
    }
}
