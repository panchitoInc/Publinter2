using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
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
        public int Add(Medio model)
        {
            model.Contactos = model.Contactos.FindAll(x => x.Delete.Equals(false) && x.Nombre != "");
            model.Programas = model.Programas.FindAll(x => x.Delete.Equals(false) && x.Nombre != "");
            return medioRepository.Add(model);
        }
        public Medio Get(int id)
        {
            return medioRepository.Get(id);
        }
       
    }
}
