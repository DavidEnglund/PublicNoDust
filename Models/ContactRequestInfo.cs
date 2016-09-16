using System;

namespace Dustbuster
{
    class ContactRequestInfo
    {
        string name;
        string phone;
        DateTime date;
        string time;
        
        public ContactRequestInfo( string name, string phone, DateTime date, string time)
        {
            this.name = name;
            this.phone = phone;
            this.date = date;
            this.time = time;
        }
    }
}
