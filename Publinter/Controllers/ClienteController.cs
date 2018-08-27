using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;

namespace Publinter.Controllers
{
    public class ClienteController : PublinteController
    {
        IClienteApplicationService _clienteApplicationService;

        private IClienteApplicationService clienteApplicationService
        {
            get
            {
                if (this._clienteApplicationService == null)
                {
                    this._clienteApplicationService = new ClienteApplicationService(CurrentUser);
                }
                return this._clienteApplicationService;
            }
        }

        public IList<Get_Cliente_Data> GetClientes()
        {
            return clienteApplicationService.GetClientes();
        }

        public ActionResult Index()
        {
            var model = clienteApplicationService.GetAll();
            return View(model);
        }

        public ActionResult Create()
        {
            Cliente model = new Cliente();
            model.Contactos = new List<Contacto>();
            for (var i = 0; i < 3; i++)
            {
                Contacto unContacto = new Contacto();
                model.Contactos.Add(unContacto);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Cliente model)
        {
            try
            {
                clienteApplicationService.Add(model);
                Cliente uncliente = new Cliente();
                uncliente.Contactos = new List<Contacto>();
                for(var i = 0; i < 3; i++)
                {
                    Contacto unc = new Contacto();
                    uncliente.Contactos.Add(unc);
                }
                
                return View(uncliente);
            }
            catch(Exception e)
            {
                return View(model);
            }
            
        }

        public ActionResult AddRenglonContacto(Cliente model)
        {
            if (model.Contactos == null) model.Contactos = new List<Contacto>();

            var html = string.Empty;
            ViewData["indexContacto"] = model.Contactos.Count == 0 ? 0 : model.Contactos.Count - 1;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Cliente/Contacto/contacto_renglon.cshtml", null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContactoRenglon(Cliente model)
        {
            var html = string.Empty;
            ViewData["indexContacto"] = model.Contactos.Count == 0 ? 0 : model.Contactos.Count - 1;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Shared/Contacto/contacto_renglon.cshtml", null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Cliente model = clienteApplicationService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Cliente model)
        {
            try
            {
                bool salida = false;
                salida = clienteApplicationService.Update(model);
                if (!salida)
                    ViewBag.error = "Ocurrio un error.";
                    return View(model);
            }
            catch (Exception)
            {
                ViewBag.error = "Ocurrio un error.";
                return View(model);
            }
        }
    }
}