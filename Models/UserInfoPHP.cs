using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dustbuster
{
    public class UserInfoPHP
    {
        public String contactInformation { get; set; }
        public DateTime requestedDate { get; set; }
        public String contactsName { get; set; }
        public String jobLocation { get; set; }
        public String contactType { get; set; }
        public String industryToContact { get; set; }

        public UserInfoPHP()
        {

        }

        public UserInfoPHP(String contactInfoz, DateTime requestDatez, String contactNamez, String jobLocationz, String contactTypez, String industryTypez)
        {
            this.contactInformation = contactInfoz;
            this.requestedDate = requestDatez;
            this.contactsName = contactNamez;
            this.jobLocation = jobLocationz;
            this.contactType = contactTypez;
            this.industryToContact = industryTypez;
        }

        private static UserInfoPHP instance;

        public static UserInfoPHP Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserInfoPHP();
                }

                return instance;
            }
        }


        public string PrintObject()
        {
            return contactInformation + " "+ requestedDate +" "+ contactsName + " "+ jobLocation + " "+ contactType +" "+ industryToContact;

        }
    }
}
