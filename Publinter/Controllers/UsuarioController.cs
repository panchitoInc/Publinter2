using AccesoDatos;
using AccesoDatos.Repository;
using BibliotecaClases.Entities;
using BusinessLogic.ApplicationServices;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publinter.Controllers
{
    public class UsuarioController : PublinteController
    {
        IUsuarioApplicationService _usuarioApplicationService;


        private IUsuarioApplicationService usuarioApplicationService
        {
            get
            {
                if (this._usuarioApplicationService == null)
                {
                    this._usuarioApplicationService = new UsuarioApplicationService(CurrentUser);
                }
                return this._usuarioApplicationService;
            }
        }


        // GET: Usuario
        public ActionResult Index()
        {

            var usuarios = usuarioApplicationService.GetAll();
            var a = 1 + 1;
            ViewBag.ListaUsuarios = usuarios;
           
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario usu)
        {

            var id = usuarioApplicationService.Create(usu);

            return Json(id , JsonRequestBehavior.AllowGet);
        }
    }
}