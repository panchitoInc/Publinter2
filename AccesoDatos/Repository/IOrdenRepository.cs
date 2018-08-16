using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModule.Entities;

namespace AccesoDatos.Repository
{
    public interface IOrdenRepository
    {
        int GetNroOrden();

        int CrearOrden(Orden nueva);
    }
}
