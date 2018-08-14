using DataModule.Entities;
using DataModule;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class MedioRepository : PublinterRepository, IMedioRepository 
    {

        public MedioRepository(AppUser Context) : base(Context)
        {

        }



        public List<Medio> GetAll()
        {

            var Medios = new List<Medio>();

            using (var context = new PublinterContext())
            {
                Medios = context.Medio
                                .Include("Contactos")
                                .Include("Programas")
                                .ToList();
            }

            return Medios.ToList();
        }
        public Medio Get(int id)
        {
            var med = new Medio();

            using (var context = new PublinterContext())
            {
                med = context.Medio
                                   .Include("Contactos")
                                   .Include("Programas") 
                                   .FirstOrDefault(x => x.MedioId == id);
            }

            return med;
        }

        public int Add(Medio model)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    context.Medio.Add(model);
                    context.SaveChanges();
                }
                return model.MedioId;

            }
            catch (Exception)
            {

                return -1;
            }
            
        }


        public bool Update(Medio model)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    var medioBd = context.Medio
                                          .FirstOrDefault(x => x.MedioId.Equals(model.MedioId));
                    if (medioBd == null) throw new Exception(String.Format("No se encontro el medio."));

                    medioBd.Descripcion = model.Descripcion;
                    medioBd.Nombre = medioBd.Nombre;

                    if (model.Contactos != null && model.Contactos.Count > 0)
                    {
                        medioBd.Contactos = model.Contactos;
                        medioBd.Contactos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                    }

                    if (model.Programas != null && model.Programas.Count() > 0)
                    {
                        medioBd.Programas = model.Programas;
                        medioBd.Programas.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            

                
        }
    }
}
