using AccesoDatos.Repository;
using DataModule;
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
    public class OrdenDeCompraApplicationService : PublinterApplicationService, IOrdenDeCompraApplicationService
    {
        IOrdenDeCompraRepository ordenDeCompraRepository;
        public OrdenDeCompraApplicationService(AppUser context) : base(context)
        {
            this.ordenDeCompraRepository = new OrdenDeCompraRepository(context);
        }
        public bool Add(OrdenDeCompra model)
        {
            try
            {
                IOrdenDeCompraRepository ordenDeCompraRepository = new OrdenDeCompraRepository(this.Context);
                return ordenDeCompraRepository.Add(model);
            }
            catch
            {
                return false;
            }

        }

        public int GetUltimoNumeroOrdenDeCompra()
        {
            return ordenDeCompraRepository.GetUltimoNumeroOrdenDeCompra();
        }
        public List<Get_index_orden_decompra> GetAll()
        {
            return ordenDeCompraRepository.GetAll();
        }

        public List<Get_OrdenDeCompra_Select> GetOrdenesDeCompraSelect(int medioId)
        {
            return ordenDeCompraRepository.GetOrdenesSelect(medioId);
        }
    }
}
