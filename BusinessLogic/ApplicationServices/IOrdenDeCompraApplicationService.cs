using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IOrdenDeCompraApplicationService
    {
        bool Add(OrdenDeCompra model);

        int GetUltimoNumeroOrdenDeCompra();

        List<Get_index_orden_decompra> GetAll();

        List<Get_OrdenDeCompra_Select> GetOrdenesDeCompraSelect(int medioId);
    }
}
