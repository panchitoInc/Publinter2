using AccesoDatos;
using AccesoDatos.Repository;
using DataModule;
using DataModule.Entities;
using Mvc;
using Publinter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

        public IList<string> GetEmailsPorMedio(int medioId)
        {
            IList<string> listaEmails = new List<string>();
            using (var context = new PublinterContext())
            {
                var _medioId = new SqlParameter("@MedioId", medioId);
                listaEmails = context.Database.SqlQuery<string>("GET_EMAILS_POR_MEDIIO @MedioId", _medioId).ToList();
            }
            return listaEmails;
        }

        public List<Medio_Model> GetMediosSelect2Ajax(int medioid, int start, string search, int length)
        {
            return medioRepository.GetMediosSelect2Ajax(medioid, start, search, length);
        }
    }
}
