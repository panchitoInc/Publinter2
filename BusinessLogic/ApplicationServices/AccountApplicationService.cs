using AccesoDatos.Repository;
using BibliotecaClases.Entities;
using ClassLibrary2.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class AccountApplicationService: IAccountApplicationService
    {


       public Usuario GetUserByNameAndPass(string email, string clave, string ip)
        {

            Usuario usuario = new Usuario();

            IAccountRepository accountRepository = new AccountRepository();
            return accountRepository.GetUserByNameAndPass(email, clave, ip);

        }
    }
}
