using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Dustbuster
{
    public class UserInfo
    {
        [PrimaryKey]
        public int ID { get; set; }  //this must be 1, simple way of ensuring the app only creates one entry
        public int UserId { get; set; }  //the id returned when adding a new web db item
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool InfoChanged { get; set; }

        public UserInfo()
        {
            ID = 1;
        }
    }
}
