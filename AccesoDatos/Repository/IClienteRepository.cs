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
        Cliente Get(int id);
        List<Get_Cliente_Data> GetClientes();
        List<Get_All_Cliente> GetAll();
        Cliente GetByRut(string rut);
        int Add(Cliente model);
        bool Update(Cliente model);
    }
}
