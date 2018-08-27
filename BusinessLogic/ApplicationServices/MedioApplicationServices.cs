using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
using Mvc;
using System;
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

            var medio = medioRepository.GetByNameAndType(model.Nombre, model.TipoMedioId??0);
            if(medio != null) throw new Exception(String.Format("El medio ya existe."));
            model.Contactos = model.Contactos.FindAll(x => x.Delete.Equals(false) && x.Nombre != "" && x.Nombre != null);
            model.Programas = model.Programas.FindAll(x => x.Delete.Equals(false) && x.Nombre != "" && x.Nombre != null);
            return medioRepository.Add(model);
        }

        public Medio Get(int id)
        {
            return medioRepository.Get(id);
        }

        public bool Update(Medio model)
        {
            return medioRepository.Update(model);
        }
       
    }
}
