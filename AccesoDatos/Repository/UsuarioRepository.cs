using AccesoDatos.Repository;
using DataModule.Entities;
using DataModule;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace AccesoDatos
{
    public class UsuarioRepository : PublinterRepository, IUsuarioRepository
    {

        public UsuarioRepository(AppUser Context) : base(Context)
        {

        }


        public Usuario ExistUser(string usuarioNombre, string clave)
        {
            Usuario _user;
            try
            {
                using (var context = new PublinterContext())
                {
                    _user = context.Usuario
                        .FirstOrDefault(x => x.NombreUsuario.Equals(usuarioNombre) && x.Password.Equals(clave));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if (_user != null) _user.Password = "";
            return _user;
        }

        public int Create(Usuario usu)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    context.Usuario.Add(usu);
                    context.SaveChanges();
                    return usu.UsuarioId;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Usuario> GetAll()
        {
            IEnumerable<Usuario> Lista;

            using (var context = new PublinterContext())
            {
                Lista = context.Usuario
                    .Include("Rol").ToList();
            }
            if (!this.Context.RolId.Equals(1))
            {
                Lista.Where(x => x.RolId != 1);
            }
            return Lista.ToList();
        }

        public Usuario Get(int id)
        {
            Usuario usu;
            using (var context = new PublinterContext())
            {
                usu = context.Usuario
                    .Include(x => x.Contactos)
                    .Include(x => x.Rol)
                    .FirstOrDefault(x => x.UsuarioId.Equals(id));
            }
                return usu;
        }
        public bool Update(Usuario usu)
        {
            return true;
        }
    }
}
