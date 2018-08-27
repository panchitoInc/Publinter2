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
        public Cliente Get(int id)
        {
            return clienteRepository.Get(id);
        }
        public List<Get_Cliente_Data> GetClientes()
        {
            return clienteRepository.GetClientes();
        }

        public List<Get_All_Cliente> GetAll()
        {
            return clienteRepository.GetAll();
        }

        public int Add(Cliente model)
        {
            try
            {
                if (Validar(model))
                {
                    model.Contactos = model.Contactos.Where(x => x.Delete.Equals(false) && x.Nombre != "").ToList();
                    clienteRepository.Add(model);
                }
                return model.ClienteId;
            }
            catch(Exception e)
            {
                throw new Exception(String.Format(e.Message));       
            }
        }

        private bool Validar(Cliente model)
        {
            bool Sinerror = false;
            if (model.RUT != "" && model.RazonSocial != "")
                Sinerror = true;
            return Sinerror;
        }
        public Cliente GetByRut(string rut)
        {
            return clienteRepository.GetByRut(rut);
        }
        public bool Update(Cliente model)
        {
            return clienteRepository.Update(model);
        }
    }
}
