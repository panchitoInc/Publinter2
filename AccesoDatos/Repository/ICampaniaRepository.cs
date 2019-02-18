using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface ICampaniaRepository
    {
        List<Get_all_campania> GetAll();
        Campania Get(int id);
        int Add(Campania model);
        bool Update(Campania model);
        List<Get_Material_Data> GetMaterialesByCampania(int campaniaId);
    }
}
