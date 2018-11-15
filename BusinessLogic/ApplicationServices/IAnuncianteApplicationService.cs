using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IAnuncianteApplicationService
    {
        Anunciante Get(int anuncianteId);

        IList<Get_Anunciante_Data> GetAnunciantes();
        int Add(Anunciante model);

    }
}
