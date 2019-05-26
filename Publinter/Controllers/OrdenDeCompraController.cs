using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publinter.Controllers
{
    public class OrdenDeCompraController : PublinteController
    {
        IOrdenDeCompraApplicationService _ordenDeCompraApplicationService;

        private IOrdenDeCompraApplicationService ordenDeCompraApplicationService
        {
            get
            {
                if (this._ordenDeCompraApplicationService == null)
                {
                    this._ordenDeCompraApplicationService = new OrdenDeCompraApplicationService(CurrentUser);
                }
                return this._ordenDeCompraApplicationService;
            }
        }
        // GET: OrdenDeCompra
        public ActionResult Index()
        {
            List<Get_index_orden_decompra> model = ordenDeCompraApplicationService.GetAll();
            return View(model);
        }
        public ActionResult Create()
        {
            IOrdenDeCompraApplicationService ordenDeCompraApplicationService = new OrdenDeCompraApplicationService(CurrentUser);
            IMedioApplicationServices medioApplicationService = new MedioApplicationServices(CurrentUser);
            ViewBag.ListaMedios = medioApplicationService.GetAll().Select(x => new SelectListItem() { Value = x.MedioId.ToString(), Text = x.Nombre});
            OrdenDeCompra model = new OrdenDeCompra();
            model.NumeroDeOrdenDeCompra = ordenDeCompraApplicationService.GetUltimoNumeroOrdenDeCompra();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrdenDeCompra model)
        {
            IMedioApplicationServices medioApplicationService = new MedioApplicationServices(CurrentUser);
            IOrdenDeCompraApplicationService ordenDeCompraApplicationService = new OrdenDeCompraApplicationService(CurrentUser);
            model.Emision = DateTime.Now;
            model.FechaVencimiento = DateTime.Now;
            ordenDeCompraApplicationService.Add(model);
            ViewBag.ListaMedios = medioApplicationService.GetAll().Select(x => new SelectListItem() { Value = x.MedioId.ToString(), Text = x.Nombre });
            OrdenDeCompra ordenC = new OrdenDeCompra();
            ordenC.NumeroDeOrdenDeCompra = ordenDeCompraApplicationService.GetUltimoNumeroOrdenDeCompra();
            return View(ordenC);
        }

    }
}