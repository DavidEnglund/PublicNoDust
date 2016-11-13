using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using Dustbuster.Models;


using Dustbuster.iOS;

[assembly: Dependency(typeof(WebServiceSPConnectClient))]
namespace Dustbuster.iOS
{
	public class WebServiceSPConnectClient : IWebServiceConnect
	{
		public Task<Boolean> TestConnection()
		{
			return Task.FromResult(false);
		}


		public Task<Boolean> AddNewRecord(String Name, String Email, String Phone)
		{
			return Task.FromResult(false);
		}

		public Task<String> GetDBVersion()
		{
			return Task.FromResult("");
		}

		public Task<Boolean> GetDB()
		{
			return Task.FromResult(false);
		}

        public Task<Boolean> SendContactDetails(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType)
        {
            return Task.FromResult(false);
        }

        public Task<Boolean> SendContactClient(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType)
        {
            return Task.FromResult(false);
        }

        public Task<Boolean> SendContactData(String contactInfo, DateTime requestDate, String contactName, String jobLocation, String contactType, String industryType)
        {
            return Task.FromResult(false);
        }
    }
}