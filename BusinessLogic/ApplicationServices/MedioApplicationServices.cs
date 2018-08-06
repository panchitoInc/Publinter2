using AccesoDatos;
using AccesoDatos.Repository;
using BibliotecaClases.Entities;
using Mvc;
using System.Collections.Generic;

namespace BusinessLogic.ApplicationServices
{
    public class MedioApplicationServices: PublinterApplicationService, IMedioApplicationServices
    {

        IMedioRepository medioRepository;

        public MedioApplicationServices(AppUser context) : base(context)
        {
            this.medioRepository = new MedioRepository(context);
        }

        public List<Medio> GetAll()
        {
            return medioRepository.GetAll();
        }
    }
}
