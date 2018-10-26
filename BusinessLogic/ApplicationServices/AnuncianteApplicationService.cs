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
    public class AnuncianteApplicationService : PublinterApplicationService, IAnuncianteApplicationService
    {
        IAnuncianteRepository anuncianteRepository;

        public AnuncianteApplicationService(AppUser context) : base(context)
        {
            this.anuncianteRepository = new AnuncianteRepository(context);
        }
        
        public Anunciante Get(int anuncianteId)
        {
            return anuncianteRepository.Get(anuncianteId);
        }

        public IList<Get_Anunciante_Data> GetAnunciantes()
        {
            return anuncianteRepository.GetAnunciantes();
        }
    }
}
