using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    // Contract that describes the dependencies being injected R.L
    public interface IWebServiceConnect
    {
        Task<Boolean> TestConnection();

        Task<Boolean> AddNewRecord(String Name, String Email, String Phone);

        Task<String> GetDBVersion();

        Task<Boolean> GetDB();

        Task<Boolean> SendContactDetails(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType);

        Task<Boolean> SendContactClient(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType);

        Task<Boolean> SendContactData(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType);
    }
}
