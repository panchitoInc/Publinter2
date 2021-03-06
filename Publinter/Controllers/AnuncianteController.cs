﻿using System;
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
    public class AnuncianteController : PublinteController
    {
        IAnuncianteApplicationService _anuncianteApplicationService;

        private IAnuncianteApplicationService anuncianteApplicationService
        {
            get
            {
                if (this._anuncianteApplicationService == null)
                {
                    this._anuncianteApplicationService = new AnuncianteApplicationService(CurrentUser);
                }
                return this._anuncianteApplicationService;
            }
        }
        public ActionResult Index()
        {
            var model = anuncianteApplicationService.GetAnunciantes();
            return View(model);
        }
        public ActionResult Create()
        {
            Anunciante model = new Anunciante();
            model.Contactos = new List<Contacto>();
            for (var i = 0; i < 3; i++)
            {
                Contacto unContacto = new Contacto();
                model.Contactos.Add(unContacto);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Anunciante model)
        {
            try
            {
                anuncianteApplicationService.Add(model);
                Anunciante uncanunciante = new Anunciante();
                uncanunciante.Contactos = new List<Contacto>();
                for (var i = 0; i < 3; i++)
                {
                    Contacto unc = new Contacto();
                    uncanunciante.Contactos.Add(unc);
                }

                return View(uncanunciante);
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View(model);
            }

        }

        public ActionResult AddRenglonContacto(Anunciante model)
        {
            if (model.Contactos == null) model.Contactos = new List<Contacto>();

            var html = string.Empty;
            ViewData["indexContacto"] = model.Contactos.Count == 0 ? 0 : model.Contactos.Count - 1;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Cliente/Contacto/contacto_renglon.cshtml", null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContactoRenglon(Anunciante model)
        {
            var html = string.Empty;
            ViewData["indexContacto"] = model.Contactos.Count == 0 ? 0 : model.Contactos.Count - 1;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Shared/Contacto/contacto_renglon.cshtml", null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public IList<Get_Anunciante_Data> GetAnunciantes()
        {
            return anuncianteApplicationService.GetAnunciantes();
        }

        public JsonResult GetOptionsAnunciantes()
        {
            IList<Get_Anunciante_Data> lista = anuncianteApplicationService.GetAnunciantes();

            string options = "<option value='0'>Todos</option>";

            foreach (var item in lista)
            {
                options += "<option value='" + item.AnuncianteId + "'>" + item.Nombre + "</option>";
            }

            return Json(new { value = true, options }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetMateriales(int anuncianteId)
        //{
        //    Anunciante unAunciante = anuncianteApplicationService.Get(anuncianteId);

        //    string html = "";

        //    if (unAunciante.Materiales != null && unAunciante.Materiales.Count > 0)
        //    {
        //        foreach (Material m in unAunciante.Materiales)
        //        {
        //            html += "<option value='" + m.MaterialId + "' data-duracion='" + m.DuracionSegundos + "'>" + m.Titulo + "</option>";
        //        }
        //    }
        //    else
        //    {
        //        html += "<option value='0' data-duracion='0'>No hay materiales</option>";
        //    }

        //    return Json(new { html }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAnuncuantesSelect2Ajax(int anuncianteId, int start, string search)
        {
            int totalRow = 0;
            int length = 20;
            start = start * length;
            List<Get_Anunciante_Data> ListaAnunciantes = anuncianteApplicationService.GetAnuncuantesSelect2Ajax(anuncianteId, start, length, search);
            if (ListaAnunciantes != null && ListaAnunciantes.Count > 0)
            {
                totalRow = ListaAnunciantes[0].TotalRows;
            }
            return Json(new { ListaAnunciantes, totalRow }, JsonRequestBehavior.AllowGet);
        }
    }
}