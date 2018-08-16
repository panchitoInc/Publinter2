using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModule.Entities;

namespace BusinessLogic.ApplicationServices
{
    public interface IOrdenApplicationService
    {
        int GetNroOrden();

        int CrearOrden(Orden nueva);
    }
}
