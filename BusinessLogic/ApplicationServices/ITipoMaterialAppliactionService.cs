using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface ITipoMaterialAppliactionService
    {
        bool Create(TipoMaterial model);
        List<TipoMaterial> GetAll();
        bool DeshabilitarTipoMaterial(int id);
    }
}
