using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface ITipoMaterialRepository
    {
        bool Create(TipoMaterial model);
        List<TipoMaterial> GetAll();
        bool DeshabilitarTipoMaterial(int id);
    }
}
