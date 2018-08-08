using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IMaterialApplicationService
    {
        int Create(Material mat);

        void Update(Material mat);

        IList<Material> GetAll();

        IList<TipoMaterial> GetTipos();
    }
}
