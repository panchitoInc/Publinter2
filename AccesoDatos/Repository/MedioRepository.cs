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

        public Medio GetByNameAndType(string nombre, int tipoMedio)
        {
            using (var context = new PublinterContext())
            {
                return context.Medio.FirstOrDefault(x => x.Nombre.Equals(nombre) && x.TipoMedioId == tipoMedio);
            }
                
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
            catch (Exception e)
            {
                var a = e.Message;
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

                    //Contactos
                    if (model.Contactos != null && model.Contactos.Count > 0)
                    {

                        // quito los vacios 
                        model.Contactos = model.Contactos.Where(x => (x.ContactoId == 0 && x.Nombre != "" && (x.Email != "" || x.Telefono != "")) || x.ContactoId > 0).ToList();

                        //Eliminados
                        var ContactosEliminados = model.Contactos.Where(x => x.Delete.Equals(true)).ToList();
                        if(ContactosEliminados.Count > 0)
                        {
                            ContactosEliminados.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Deleted);
                            context.SaveChanges();
                        }
                        // los viejos que se pudireran modificar
                        var contactosModificados = model.Contactos.Where(x => x.ContactoId > 0 && x.Delete.Equals(false)).ToList();
                        if (contactosModificados.Count > 0)
                        {
                            UpdateContacto(medioBd, contactosModificados);
                            medioBd.Contactos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                            context.SaveChanges();
                        }
                        // contactos Nuevos
                        var contactosNuevos = model.Contactos.Where(x => x.Delete.Equals(false) && x.ContactoId == 0 && x.Nombre != "" && (x.Telefono != "" || x.Email != "")).ToList();
                        if (contactosNuevos.Count > 0)
                        {
                            SaveNewContacto(medioBd, contactosNuevos);

                            contactosNuevos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Added);
                            context.SaveChanges();
                        }

                        
                        
                    }

                    // Programas
                    if (model.Programas != null && model.Programas.Count() > 0)
                    {
                        //quiton los vacios.
                        model.Programas = model.Programas.Where(x => x.Nombre !="" && x.PrecioSegundo > 0 && x.Duracion > 0 ).ToList();

                        // Programas eliminados
                        var ProgramasEliminados = medioBd.Programas.Where(x => x.Equals(true)).ToList();
                        if(ProgramasEliminados.Count() > 0)
                        {
                            ProgramasEliminados.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Deleted);
                            context.SaveChanges();
                        }

                        //Programas nuevos
                        var ProgramasNuevos = model.Programas.Where(x =>x.Delete.Equals(false) && x.ProgramaId == 0 && x.Nombre != "" && x.HoraInicio  != null && x.PrecioSegundo > 0).ToList();
                        if(ProgramasNuevos.Count > 0)
                        {
                            SaveNewPrograma(medioBd, ProgramasNuevos);
                        }
                        context.SaveChanges();
                        //viejos modificados
                        var ProgramasModificados = model.Programas.Where(x => x.ProgramaId > 0 && x.Delete.Equals(false)).ToList();
                        if(ProgramasModificados.Count > 0)
                        {
                            UpdatePrograma(medioBd, ProgramasModificados);
                        }
                        medioBd.Programas.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                        context.SaveChanges();
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
                    existingInBd.Nombre = contAct.Nombre;
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

        public void SaveNewPrograma(Medio medioBd, List<Programa> nuevos)
        {
            foreach (var p in nuevos)
            {
                medioBd.Programas.Add(p);
            }

        }

        public void UpdatePrograma(Medio mediobd, List<Programa> Nuevos)
        {
            foreach (var progAct in Nuevos)
            {
                var existingInBd = mediobd.Programas
                                          .Where(p => p.ProgramaId == progAct.ProgramaId)
                                          .SingleOrDefault();

                if(existingInBd != null)
                {
                    existingInBd.Duracion = progAct.Duracion;
                    existingInBd.HoraInicio = progAct.HoraInicio;
                    existingInBd.Nombre = progAct.Nombre;
                    existingInBd.PrecioSegundo = progAct.PrecioSegundo;
                    existingInBd.ProgramaId = progAct.ProgramaId;
                }
            }

        }
    }
}
