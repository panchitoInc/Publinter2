using DataModule;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class ClienteRepository : PublinterRepository, IClienteRepository
    {
        public ClienteRepository(AppUser Context) : base(Context)
        {

        }

        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes;

            using (var context = new PublinterContext())
            {
                var _usuId = new SqlParameter("@UsarioId", this.Context.Id);
                clientes = context.Database.SqlQuery<Cliente>("GET_CLIENTES @UsarioId", _usuId).ToList();
            }

            return clientes;
        }
    }
}
