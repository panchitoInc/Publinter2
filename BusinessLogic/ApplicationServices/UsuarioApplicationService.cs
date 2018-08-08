using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
using Mvc;
using System.Collections.Generic;

namespace BusinessLogic.ApplicationServices
{
    public class UsuarioApplicationService: PublinterApplicationService, IUsuarioApplicationService
    {
        IUsuarioRepository usuarioRepository;

        public UsuarioApplicationService(AppUser context) : base(context)
        {
            this.usuarioRepository = new UsuarioRepository(context);
        }
  
        public int Create(Usuario usu)
        {
            int id = 0;
            if(Validar(usu))
                id = usuarioRepository.Create(usu);
            return id;
        }

        public bool Validar(Usuario usu)
        {
            bool valido = false;

            var usuDb = usuarioRepository.ExistUser(usu.NombreUsuario,usu.Password);
            if (usuDb == null && usu.Nombre != "" && usu.Apellido != "")
                valido = true;
            return valido;
        }

        public IList<Usuario> GetAll()
        {
            return usuarioRepository.GetAll();
        }
    }
}
