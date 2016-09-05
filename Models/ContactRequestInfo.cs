using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    class ContactRequestInfo
    {
        string name;
        string phone;
        DateTime date;
        string time;
        bool reminder;

        public ContactRequestInfo( string name, string phone, DateTime date, string time)
        {
            this.name = name;
            this.phone = phone;
            this.date = date;
            this.time = time;
        }
    }
}
