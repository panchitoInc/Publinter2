using DataModule.Entities;
using DataModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class AccountRepository: IAccountRepository
    {

        public Usuario GetUserByNameAndPass(string usuario, string clave,string ip)
        {
            Usuario _user;

            using (var context = new PublinterContext())
            {
                _user = context.Usuario
                    .Include("Rol")
                   .FirstOrDefault(x => x.NombreUsuario.Equals(usuario) && x.Password.Equals(clave));
                UsuarioAccess useraccess = new UsuarioAccess
                {
                    User = usuario

                };

                if (_user == null)
                {
                    useraccess.Msg = "Usuario no encontrado o contraseña incorrecta.";
                    useraccess.TypeAccess = 2;
                }
                else
                {
                    
                    useraccess.UsuarioId = _user.UsuarioId;
                    useraccess.Msg = "Acceso concedido.";
                    useraccess.TypeAccess = 1;
                    
                }
                context.UsuarioAccess.Add(useraccess);
                context.SaveChanges();
            }

            if (_user != null) _user.Password = "";
            return _user;
        }
    }
}
