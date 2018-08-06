using BibliotecaClases.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IUsuarioRepository
    {

        /// <summary>
        /// Comprueba que noexista ya el usuario en a base de datos.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        Usuario ExistUser(string email, string clave);
        
        /// <summary>
        /// Create usuario
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        int Create(Usuario usu);

        /// <summary>
        /// Retorna la ista de USsuarios
        /// </summary>
        /// <returns></returns>
        List<Usuario> GetAll();
    }
}
