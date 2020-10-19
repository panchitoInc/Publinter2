using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
using BusinessLogic.ApplicationServices;
using Mvc;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using DataModule.EntitiesResult;
using System.Linq;

namespace Publinter.Controllers
{
    public class MedioController : PublinteController {

        IMedioApplicationServices _medioApplicationService;
        IProgramaApplicationService _programaAplicacionService;


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

        private IProgramaApplicationService programaAplicacionService
        {
            get
            {
                if (this._programaAplicacionService == null)
                {
                    this._programaAplicacionService = new ProgramaApplicationService(CurrentUser);
                }
                return this._programaAplicacionService;
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

        [HttpPost]
        public JsonResult GetProgramas(int medioId)
        {
            Medio m = medioApplicationService.Get(medioId);
            var programas = programaAplicacionService.GetProgramasByMedio(medioId);
            string html = "";

            if (programas != null && programas.Count > 0)
            {
                foreach (Get_Programa_Data p in programas)
                {
                    html += "<option value='" + p.ProgramaId + "' data-precio='" + p.Precio + "'>" + p.Nombre + "</option>";
                }
            }
            else
            {
                html += "<option value='0' data-precio='0'>No hay programas</option>";
            }

            return Json(new { html }, JsonRequestBehavior.AllowGet);
        }

        public  JsonResult GetEmailsPorMedio(int medioId)
        {
            string html = string.Empty;
            html += "<option value='-1'>Seleccione un Email</option>";
            var ListaMediosEmails = medioApplicationService.GetEmailsPorMedio(medioId);
            if (ListaMediosEmails != null)
            {
                foreach(var email in ListaMediosEmails)
                {
                    html += "<option value='" + email + "'>" + email + "</option>";
                }
            }

            return Json(new { html }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionsMedios()
        {
            IList<Get_Medio_Data> lista = medioApplicationService.GetMedios();

            string options = "<option value='0'>Todos</option>";

            foreach (var item in lista)
            {
                options += "<option value='" + item.MedioId + "'>" + item.Nombre + "</option>";
            }

            return Json(new { value = true, options }, JsonRequestBehavior.AllowGet);
        }
    }
}