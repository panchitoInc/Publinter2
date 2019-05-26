using AccesoDatos.Repository;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class TipoMaterialAppliactionService : PublinterApplicationService ,ITipoMaterialAppliactionService
    { 
        public TipoMaterialAppliactionService(AppUser context) : base(context)
        {

        }
        public bool Create(TipoMaterial model)
        {
            ITipoMaterialRepository tipoMaterialRepository = new TipoMaterialRepository(this.Context);
            model.Deshabilitado = false;
            return tipoMaterialRepository.Create(model);
        }

        public List<TipoMaterial> GetAll()
        {
            ITipoMaterialRepository tipoMaterialRepository = new TipoMaterialRepository(this.Context);
            List<TipoMaterial> model = tipoMaterialRepository.GetAll();
            return model;
        }

        public bool DeshabilitarTipoMaterial(int id)
        {
            ITipoMaterialRepository tipoMaterialRepository = new TipoMaterialRepository(this.Context);
            return tipoMaterialRepository.DeshabilitarTipoMaterial(id);
        }
    }
}
