using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var usu = usuarioApplicationService.Get(id);
            return View(usu);
        }
        [HttpPost]
        public ActionResult Edit(Usuario usu)
        {
            var bandera = usuarioApplicationService.Update(usu);
            return View();
        }
    }
}