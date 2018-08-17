using AccesoDatos.Repository;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class ClienteApplicationService : PublinterApplicationService, IClienteApplicationService
    {
        IClienteRepository clienteRepository;

        public ClienteApplicationService(AppUser context) : base(context)
        {
            this.clienteRepository = new ClienteRepository(context);
        }

        public List<Cliente> GetClientes()
        {
            return clienteRepository.GetClientes();
        }
    }
}
