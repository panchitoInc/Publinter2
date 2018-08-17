using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IProgramaRepository
    {
        List<Get_Programa_Data> GetProgramas();
    }
}
