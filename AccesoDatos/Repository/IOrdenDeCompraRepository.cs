using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IOrdenDeCompraRepository
    {
        bool Add(OrdenDeCompra model);
        int GetUltimoNumeroOrdenDeCompra();
        List<Get_index_orden_decompra> GetAll();
    }
}
