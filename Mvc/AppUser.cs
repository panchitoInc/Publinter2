
namespace Mvc
{
    using System.Security.Claims;

    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {

        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Email
        {
            get
            {
                return this.FindFirst(ClaimTypes.Email).Value;
            }
        }



        public string UserData
        {
            get
            {
                return this.FindFirst("NombreUsuario").Value;
            }
        }

        public int Id
        {
            get
            {
                return int.Parse(this.FindFirst("Id").Value);
            }
        }

        public string UserAccess
        {
            get
            {
                return this.FindFirst("UserAccess").Value;
            }
        }

        public int RolId
        {
            get
            {
                return int.Parse(this.FindFirst(ClaimTypes.Role).Value);
            }
        }

        public string RolDescripcion
        {
            get
            {
                return this.FindFirst("RolDescripcion").Value;
                
            }
        }

        public string Apellido
        {
            get
            {
                return this.FindFirst("UserApellido").Value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                var value = false;

                if (this.FindFirst(ClaimTypes.Role).Value == "1")
                {
                    value = true;
                }
                return value;
            }
        }
    }

}
