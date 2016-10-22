using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster.Interfaces
{
    // Contract that describes the dependencies used
    public interface IWebServiceConnect
    {
        Task<Boolean> TestConnection();

        Task<Boolean> AddNewRecord(String Name, String Email, String Phone);

        Task<String> GetDBVersion();
    }
}
