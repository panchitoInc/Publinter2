using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
using BusinessLogic.ApplicationServices;
using Mvc;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace Publinter.Controllers
{
    public class MedioController : PublinteController {

        IMedioApplicationServices _medioApplicationService;

        private IMedioApplicationServices medioApplicationService
        {
            get
            {
                if (this._medioApplicationService == null)
                {
                    this._medioApplicationService = new MedioApplicationServices(CurrentUser);
                }
                return this._medioApplicationService;
            }
        }

        // GET: Medio
        public ActionResult Index()
        {
            List<Medio> ListaMedios = medioApplicationService.GetAll();

            return View(ListaMedios);
        }

        public JsonResult AddMedioRenglon(Medio medio)
        {
            var html = string.Empty;
            ViewData["indexContacto"] = medio.Contactos.Count == 0 ? 0 : medio.Contactos.Count;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Medio/Contacto/_medio_contacto_renglon.cshtml", null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            Medio model = new Medio();

            for (var i = 0; i < 3; i++)
            {
                Contacto c = new Contacto();
                model.Contactos.Add(c);
                Programa p = new Programa();
                model.Programas.Add(p);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Medio model)
        {
            try{
                var id = medioApplicationService.Add(model);

                if (id == 0)
                {
                    ViewBag.error = "no se puede guardar";
                    return View(model);
                }
                
                return RedirectToAction("Create");
            }
            catch(Exception e)
            {
                ViewBag.error = e.Message;
                return View(model);
            }
            
        }

        [HttpPost]
        public JsonResult AddProgramaRenglon(Medio medio)
        {
            var html = string.Empty;
            ViewData["indexPrograma"] = medio.Programas.Count == 0 ? 0 : medio.Programas.Count;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Medio/Programa/_medio_programa_renglon.cshtml", null);
            return Json(new {html = html, index = medio.Programas.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Medio unMedio = medioApplicationService.Get(id);

            return View(unMedio);
        }

        [HttpPost]
        public ActionResult Edit(Medio model)
        {
            var bandera = medioApplicationService.Update(model);
            if (bandera)
            {
                ViewBag.error = "Ocurrio un error al guardar el medio";
            }
            
            return View(model);
        }

        public JsonResult GetProgramas(int medioId)
        {
            Medio m = medioApplicationService.Get(medioId);

            string html = "";

            if (m.Programas != null && m.Programas.Count > 0)
            {
                foreach (Programa p in m.Programas)
                {
                    html += "<option value='" + p.ProgramaId + "' data-precio='" + p.PrecioSegundo + "'>" + p.Nombre + "</option>";
                }
            }
            else
            {
                html += "<option value='0' data-precio='0'>No hay programas</option>";
            }

            return Json(new { html }, JsonRequestBehavior.AllowGet);
        }
    }
}