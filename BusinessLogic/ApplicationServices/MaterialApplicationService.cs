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
    public class MaterialApplicationService : PublinterApplicationService, IMaterialApplicationService
    {
        IMaterialRepository materialRepository;

        public MaterialApplicationService(AppUser context) : base(context)
        {
            this.materialRepository = new MaterialRepository(context);
        }

        public int Create(Material mat)
        {
            return materialRepository.Create(mat);
        }

        public IList<Material> GetAll()
        {
            return materialRepository.GetAll();
        }

        public IList<TipoMaterial> GetTipos()
        {
            return materialRepository.GetTipos();
        }

        public void Update(Material mat)
        {
            materialRepository.Update(mat);
        }
    }
}
