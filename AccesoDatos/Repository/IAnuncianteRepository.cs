using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IAnuncianteRepository
    {
        Anunciante Get(int id);

        List<Get_Anunciante_Data> GetAnunciantes();

        int Add(Anunciante model);

        bool Update(Anunciante model);
    }
}
