using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface ICampaniaApplicationService
    {
        List<Get_all_campania> GetAll();
        Campania Get(int id);
        int Add(Campania model);
        bool Edit(Campania model);
        List<Get_Material_Data> GetMaterialesByCampania(int campaniaId);
    }
}
