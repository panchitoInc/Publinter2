using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IClienteRepository
    {
        List<Cliente> GetClientes();
    }
}
