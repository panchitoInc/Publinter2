using AccesoDatos.Repository;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class OrdenApplicationService : PublinterApplicationService, IOrdenApplicationService
    {
        IOrdenRepository ordenRepository;

        public OrdenApplicationService(AppUser context) : base(context)
        {
            this.ordenRepository = new OrdenRepository(context);
        }

        public int GetNroOrden()
        {
            return ordenRepository.GetNroOrden();
        }

        public int CrearOrden(Orden nueva)
        {
            return ordenRepository.CrearOrden(nueva);
        }
    }
}
