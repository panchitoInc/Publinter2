using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.ApplicationServices
{
    public interface IAccountApplicationService
    {

      Usuario GetUserByNameAndPass(string email, string clave,string ip);
    }
}
