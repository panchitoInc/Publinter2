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
                    medioBd.Nombre = model.Nombre;
                    if (model.Contactos != null && model.Contactos.Count > 0)
                    {

                        // quito los vacios 
                        model.Contactos = model.Contactos.Where(x => x.ContactoId > 0 && x.Nombre != "").ToList();

                        // contactos Nuevos
                        var contactosNuevos = model.Contactos.Where(x => x.Delete.Equals(false) && x.ContactoId == 0 && x.Nombre != "" && (x.Telefono != "" || x.Email != "")).ToList();
                        if (contactosNuevos != null)
                        {
                            SaveNewContacto(medioBd, contactosNuevos);
                        }

                        // los viejos que se pudireran modificar
                        var contactosModificados = model.Contactos.Where(x => x.ContactoId > 0 && x.Delete.Equals(false)).ToList();
                        if(contactosModificados != null)
                        {
                            UpdateContacto(medioBd, contactosModificados);
                        }

                        //medioBd.Contactos = model.Contactos.Where(x => x.Delete.Equals(false).ToList();
                        medioBd.Contactos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                    }

                    context.SaveChanges();
                    // Programas
                    if (model.Programas != null && model.Programas.Count() > 0)
                    {
                        //quiton los vacios.
                        medioBd.Programas = model.Programas.Where(x => x.ProgramaId > 0 && x.Nombre !="").ToList();
                        var ProgramasNuevos = model.Programas.Where(x =>x.Delete.Equals(false) && x.ProgramaId == 0 && x.Nombre != "" && x.HoraInicio  != null && x.PrecioSegundo > 0).ToList();

                        medioBd.Programas.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                    }
                    
                    context.Entry(medioBd).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public void UpdateContacto(Medio mediobd, List<Contacto>Nuevos)
        {
            foreach(var contAct in Nuevos)
            {
                var existingInBd = mediobd.Contactos
                                    .Where(c => c.ContactoId == contAct.ContactoId)
                                    .SingleOrDefault();

                if(existingInBd != null)
                {
                    existingInBd.Ciudad = contAct.Ciudad;
                    existingInBd.Departamento = contAct.Ciudad;
                    existingInBd.Direccion = contAct.Direccion;
                    existingInBd.Email = contAct.Email;
                    existingInBd.Telefono = contAct.Telefono;
                }

            }

        }

        public void SaveNewContacto(Medio medioBd, List<Contacto> nuevos)
        {
            foreach(var c in nuevos)
            {
                medioBd.Contactos.Add(c);
            }
            
        }
    }
}
