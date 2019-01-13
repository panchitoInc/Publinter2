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
    public class ClienteRepository : PublinterRepository, IClienteRepository
    {
        public ClienteRepository(AppUser Context) : base(Context)
        {

        }

        public Cliente Get(int id)
        {
            Cliente CliBuscado;

            using (var context = new PublinterContext())
            {
                CliBuscado = context.Cliente
                                            .Include("Contactos")
                                            .FirstOrDefault(x => x.ClienteId.Equals(id));
            }
            return CliBuscado;
        }
        public List<Get_Cliente_Data> GetClientes()
        {
            List<Get_Cliente_Data> clientes;

            using (var context = new PublinterContext())
            {
                //var _usuId = new SqlParameter("@UsarioId", this.Context.Id);
                clientes = context.Database.SqlQuery<Get_Cliente_Data>("GET_ALL_CLIENTES ").ToList();
            }

            return clientes;
        }

        public List<Get_All_Cliente> GetAll()
        {
            List<Get_All_Cliente> clientes;

            using (var context = new PublinterContext())
            {
                clientes = context.Database.SqlQuery<Get_All_Cliente>("GET_ALL_CLIENTES").ToList();
            }
            return clientes;
        }

        public Cliente GetByRut(string rut)
        {
            Cliente uncli;
            using (var context = new PublinterContext())
            {
                uncli = context.Cliente.FirstOrDefault(x => x.Equals(rut));
            }
            return uncli;
        }
        public int Add(Cliente model)
        {
            using (var context = new PublinterContext())
            {
                var CliBd = Get(model.ClienteId);
                if (CliBd != null) throw new Exception(String.Format("El cliente ya existe"));
                
                context.Cliente.Add(model);
                context.SaveChanges();
                return model.ClienteId;
            }
        }

        public bool Update(Cliente model)
        {
            try
            {
                using (var context =new PublinterContext())
                {
                    var cliBd = Get(model.ClienteId);
                    if (cliBd == null) throw new Exception(string.Format("No se encontro el cliente"));

                    cliBd.Nombre = model.Nombre;
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
                            UpdateContacto(cliBd, contactosModificados);
                            cliBd.Contactos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                            context.SaveChanges();
                        }

                        // contactos Nuevos
                        var contactosNuevos = model.Contactos.Where(x => x.Delete.Equals(false) && x.ContactoId == 0 && x.Nombre != "" && (x.Telefono != "" || x.Email != "")).ToList();
                        if (contactosNuevos.Count > 0)
                        {
                            SaveNewContacto(cliBd, contactosNuevos);
                            contactosNuevos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Added);
                            context.SaveChanges();
                        }
                    }
                    context.Entry(cliBd).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }

        public void SaveNewContacto(Cliente cliBd, List<Contacto> nuevos)
        {
            foreach (var c in nuevos)
            {
                cliBd.Contactos.Add(c);
            }

        }
        public void UpdateContacto(Cliente mediobd, List<Contacto> Nuevos)
        {
            foreach (var contAct in Nuevos)
            {
                var existingInBd = mediobd.Contactos
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
