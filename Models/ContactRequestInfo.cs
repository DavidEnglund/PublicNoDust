using System;

namespace Dustbuster
{
    public class ContactRequestInfo
    {
		public string Name;
		public string Contact;
        public DateTime Date;
        
        
        public ContactRequestInfo( string name, string contact, DateTime date)
        {
            this.Name = name;
            this.Contact = contact;
            this.Date = date;            
        }
    }
}
