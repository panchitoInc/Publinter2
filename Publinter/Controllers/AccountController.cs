using DataModule.Entities;
using BusinessLogic.ApplicationServices;
using ClassLibrary2.ApplicationServices;
using Mvc;
using Publinter.Models;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;

namespace Publinter.Controllers
{
    public class AccountController : PublinteController
    {
        string PublinterCookie = "PublinterCookie";
        IAccountApplicationService _accountApplicationService;

        private IAccountApplicationService accountApplicationservice
        {
            get
            {
                if (this._accountApplicationService == null)
                {
                    this._accountApplicationService = new AccountApplicationService();
                }
                return this._accountApplicationService;
            }
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            bool access = false;

            string ip = Request.UserHostAddress;
            
            Usuario _usuario = accountApplicationservice.GetUserByNameAndPass(model.Usuario, model.clave,ip);
            access = (_usuario != null);
            
            if (access)
            {

                
                ClaimsIdentity identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, _usuario.Nombre + " " +  _usuario.Apellido),
                    new Claim("NombreUsuario", _usuario.NombreUsuario),
                    new Claim(ClaimTypes.Sid, _usuario.UsuarioId.ToString()),
                    new Claim(ClaimTypes.PrimarySid, _usuario.UsuarioId.ToString()),
                    new Claim("Id",_usuario.UsuarioId.ToString()),
                    new Claim("UserApellido", _usuario.Apellido),
                    new Claim("Rol", _usuario.RolId.ToString()),
                    new Claim("RolDescripcion", _usuario.Rol.Descripcion)

                    }, PublinterCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);
                

                return RedirectToAction("Index","Usuario");//CheckIsDefaultPass(model, _usuario);
            }
            ViewBag.error = "Usuario o password invalida.";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            ClearUser();
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut(PublinterCookie);
            return RedirectToAction("Login", "Account");
        }

        public void ClearUser()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
    }
}