﻿using AccesoDatos.Repository;
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

        public int Add(Anunciante model)
        {
            try
            {
                if (Validar(model))
                {
                    model.Contactos = model.Contactos.Where(x => x.Delete.Equals(false) && x.Nombre != "" && x.Nombre != null  ).ToList();
                    anuncianteRepository.Add(model);
                }
                return model.AnuncianteId;
            }
            catch (Exception e)
            {
                throw new Exception(String.Format(e.Message));
            }
        }

        private bool Validar(Anunciante model)
        {
            bool Sinerror = true;

            //validaciones

            return Sinerror;
        }

        public Anunciante GetByRut(string rut)
        {
            return anuncianteRepository.GetByRut(rut);
        }
        public bool Update(Anunciante model)
        {
            return anuncianteRepository.Update(model);
        }

        public List<Get_Anunciante_Data> GetAnuncuantesSelect2Ajax(int anuncianteId, int start, int length,string search)
        {
            return anuncianteRepository.GetAnuncuantesSelect2Ajax(anuncianteId, start,length, search);
        }
    }
}
