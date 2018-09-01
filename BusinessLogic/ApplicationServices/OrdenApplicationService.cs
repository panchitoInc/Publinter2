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

        public IEnumerable<Get_orden_index> GetIndex(int start, int length, int sortColumn, string sortDirection, string search)
        {
            return ordenRepository.GetIndex(start, length, sortColumn, sortDirection, search);
        }
    }
}
