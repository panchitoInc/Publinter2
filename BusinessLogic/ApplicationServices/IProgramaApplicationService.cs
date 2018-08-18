using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IProgramaApplicationService
    {
        List<Get_Programa_Data> GetProgramas();
    }
}
