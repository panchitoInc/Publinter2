using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModule.Entities;
using DataModule.EntitiesResult;

namespace BusinessLogic.ApplicationServices
{
    public interface IOrdenApplicationService
    {
        int GetNroOrden();

        int CrearOrden(Orden nueva);

        IEnumerable<Get_orden_index> GetIndex(int start, int length, int sortColumn, string sortDirection, string search);
    }
}
