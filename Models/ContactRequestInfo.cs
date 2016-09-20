using System;

namespace Dustbuster
{
    class ContactRequestInfo
    {
        string name;
        string contact;
        DateTime date;
        
        
        public ContactRequestInfo( string name, string contact, DateTime date)
        {
            this.name = name;
            this.contact = contact;
            this.date = date;            
        }
    }
}
