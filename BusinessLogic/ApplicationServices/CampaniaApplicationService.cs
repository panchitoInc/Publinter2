using AccesoDatos.Repository;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class CampaniaApplicationService: PublinterApplicationService,ICampaniaApplicationService
    {
        ICampaniaRepository campaniarepository;
        public CampaniaApplicationService(AppUser context) :base(context)
        {
            this.campaniarepository = new CampaniaRepository(context);
        }
        public List<Get_all_campania> GetAll()
        {
            return campaniarepository.GetAll();
        }
        public Campania Get(int id)
        {
            Campania campaniaBd = campaniarepository.Get(id);
            return campaniaBd;
        }

        public int Add(Campania model)
        {
            return campaniarepository.Add(model);
            
        }

        public bool Edit(Campania model)
        {
            model.Materiales = model.Materiales.Where(x => x.DuracionSegundos > 0 && x.Titulo != "").ToList();

            return campaniarepository.Update(model);

        }

        public List<Get_Material_Data> GetMaterialesByCampania(int campaniaId)
        {
            return campaniarepository.GetMaterialesByCampania(campaniaId);
        }
    }
}
