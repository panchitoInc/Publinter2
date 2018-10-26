using DataModule;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class AnuncianteRepository : PublinterRepository, IAnuncianteRepository
    {
        public AnuncianteRepository(AppUser Context) : base(Context)
        {

        }

        public int Add(Anunciante model)
        {
            using (var context = new PublinterContext())
            {
                var AnuncianteBd = Get(model.AnuncianteId);
                if (AnuncianteBd != null) throw new Exception(String.Format("El anunciante ya existe"));

                context.Anunciante.Add(model);
                context.SaveChanges();
                return model.AnuncianteId;
            }
        }

        public Anunciante Get(int id)
        {
            Anunciante buscado;

            using (var context = new PublinterContext())
            {
                buscado = context.Anunciante
                                            .Include("Contactos")
                                            .Include("Materiales")
                                            .FirstOrDefault(x => x.AnuncianteId.Equals(id));
            }
            return buscado;
        }

        public List<Get_Anunciante_Data> GetAnunciantes()
        {
            List<Get_Anunciante_Data> anunciantes;

            using (var context = new PublinterContext())
            {
                var _usuId = new SqlParameter("@UsarioId", this.Context.Id);
                anunciantes = context.Database.SqlQuery<Get_Anunciante_Data>("GET_ANUNCIANTES @UsarioId", _usuId).ToList();
            }

            return anunciantes;
        }

        public bool Update(Anunciante model)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    var anuncianteBd = Get(model.AnuncianteId);
                    if (anuncianteBd == null) throw new Exception(string.Format("No se encontro el anunciante"));

                    anuncianteBd.Nombre = model.Nombre;
                    //cliBd.RazonSocial = model.RazonSocial;
                    //cliBd.RUT = model.RUT;

                    // Contactos
                    if (model.Contactos != null && model.Contactos.Count > 0)
                    {
                        // Quito Vacios
                        model.Contactos = model.Contactos.Where(x => (x.ContactoId == 0 && x.Nombre != "" && (x.Email != "" || x.Telefono != "")) || x.ContactoId > 0).ToList();

                        //Eliminados, vacios o mal completos.
                        var ContactosEliminados = model.Contactos.Where(x => x.Delete.Equals(true)).ToList();
                        if (ContactosEliminados.Count > 0)
                        {
                            ContactosEliminados.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Deleted);
                            context.SaveChanges();
                        }

                        // los viejos que se pudireran modificar
                        var contactosModificados = model.Contactos.Where(x => x.ContactoId > 0 && x.Delete.Equals(false)).ToList();
                        if (contactosModificados.Count > 0)
                        {
                            UpdateContacto(anuncianteBd, contactosModificados);
                            anuncianteBd.Contactos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                            context.SaveChanges();
                        }

                        // contactos Nuevos
                        var contactosNuevos = model.Contactos.Where(x => x.Delete.Equals(false) && x.ContactoId == 0 && x.Nombre != "" && (x.Telefono != "" || x.Email != "")).ToList();
                        if (contactosNuevos.Count > 0)
                        {
                            SaveNewContacto(anuncianteBd, contactosNuevos);
                            contactosNuevos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Added);
                            context.SaveChanges();
                        }
                    }

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void SaveNewContacto(Anunciante anuncianteBd, List<Contacto> nuevos)
        {
            foreach (var c in nuevos)
            {
                anuncianteBd.Contactos.Add(c);
            }
        }

        public void UpdateContacto(Anunciante anunciantebd, List<Contacto> Nuevos)
        {
            foreach (var contAct in Nuevos)
            {
                var existingInBd = anunciantebd.Contactos
                                    .Where(c => c.ContactoId == contAct.ContactoId)
                                    .SingleOrDefault();

                if (existingInBd != null)
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
    }
}
