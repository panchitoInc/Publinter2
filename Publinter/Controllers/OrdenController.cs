using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.tool.xml;
using Mvc;
using Publinter.Models;

namespace Publinter.Controllers
{
    public class OrdenController : PublinteController
    {
        IOrdenApplicationService _ordenApplicationService;
        IMedioApplicationServices _medioApplicationService;
        IMaterialApplicationService _materialApplicationService;
        IClienteApplicationService _clienteApplicationService;
        IProgramaApplicationService _programaApplicationService;
        ICampaniaApplicationService _campaniaAplicationService;

        private ICampaniaApplicationService campaniaAplicationService
        {
            get
            {
                if (this._campaniaAplicationService == null)
                {
                    this._campaniaAplicationService = new CampaniaApplicationService(CurrentUser);
                }
                return this._campaniaAplicationService;
            }
        }
        private IOrdenApplicationService ordenApplicationService
        {
            get
            {
                if (this._ordenApplicationService == null)
                {
                    this._ordenApplicationService = new OrdenApplicationService(CurrentUser);
                }
                return this._ordenApplicationService;
            }
        }

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

        private IMaterialApplicationService materialApplicationService
        {
            get
            {
                if (this._materialApplicationService == null)
                {
                    this._materialApplicationService = new MaterialApplicationService(CurrentUser);
                }
                return this._materialApplicationService;
            }
        }

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

        private IProgramaApplicationService programaApplicationService
        {
            get
            {
                if (this._programaApplicationService == null)
                {
                    this._programaApplicationService = new ProgramaApplicationService(CurrentUser);
                }
                return this._programaApplicationService;
            }
        }

        public ActionResult Create()
        {
            Orden_Create_Model model = new Orden_Create_Model();
            model.ListaCampanias = campaniaAplicationService.GetAll();
            var PrimerCampania = model.ListaCampanias.FirstOrDefault();
            var CampaniaConDependencias = campaniaAplicationService.Get(PrimerCampania.CampaniaId);
            model.ListaMateriales = CampaniaConDependencias.Materiales;
            model.ListaMedios = medioApplicationService.GetAll();
            model.ListaProgramas = programaApplicationService.GetProgramasByMedio(model.ListaMedios.FirstOrDefault().MedioId);
            model.ListaClientes = clienteApplicationService.GetClientes();


            model.NroOrden = ordenApplicationService.GetNroOrden();
            model.UsuarioId = CurrentUser.Id;

            return View(model);
        }

        [HttpPost]
        public JsonResult Create(Orden_Create_Model model)
        {
            bool value;

            try
            {
                Orden nueva = model.ToOrden();
                ordenApplicationService.CrearOrden(nueva);

                value = true;

                return Json(new { value }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                value = false;

                string errorMsg = "Error al crear la oren : " + e.Message;

                return Json(new { value, errorMsg }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Orden
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetDataOrdenIndex(int draw, int start, int length)
        {
            int TOTAL_ROWS = 0;
            string search = Request["search[value]"];
            int sortColumn = 0;
            string sortDirection = "asc";

            if (Request["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request["order[0][column]"]);
            }
            if (Request["order[0][dir]"] != null)
            {
                sortDirection = Request["order[0][dir]"];
            }

            DataTableDataOrden dataTableData = new DataTableDataOrden();

            dataTableData.draw = draw;

            var ordenes = ordenApplicationService.GetIndex(start, length, sortColumn, sortDirection, search).ToList();

            if (ordenes != null && ordenes.Count > 0)
            {
                TOTAL_ROWS = ordenes[0].TotalRows;
            }
            else
            {
                TOTAL_ROWS = 0;
            }
            dataTableData.recordsTotal = TOTAL_ROWS;

            dataTableData.data = ordenes;
            dataTableData.recordsFiltered = TOTAL_ROWS;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarPrimeraLinea(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = 0;

            viewModel.IndexLinea = cantLineas;

            LineaOrden nueva = new LineaOrden();
            nueva.LineasInternasOrden = new List<LineaInternaOrden>();

            LineaInternaOrden primera = new LineaInternaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = DateTime.Now.Year;
            mesActual.MesNumero = DateTime.Now.AddMonths(1).Month;
            mesActual.MesNombre = this.GetMesNombre(mesActual.MesNumero);

            mesActual.Dias = new List<Dia>();

            int nroDias = DateTime.DaysInMonth(mesActual.MesAnio, mesActual.MesNumero);

            for (int i = 0; i < nroDias; i++)
            {
                Dia dia = new Dia();
                dia.DiaNumero = i + 1;

                DateTime fecha = new DateTime();
                string fechaString = dia.DiaNumero + "/" + mesActual.MesNumero + "/" + DateTime.Now.Year;
                DateTime.TryParseExact(fechaString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
                dia.DiaNombre = this.GetDiaNombre(fecha.DayOfWeek.ToString());

                dia.NroEmisiones = 0;
                dia.TotalDia = 0;

                mesActual.Dias.Add(dia);
            }

            primera.Mes = mesActual;

            nueva.LineasInternasOrden.Add(primera);

            viewModel.Lineas.Add(nueva);

            viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = 0;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarSiguienteMes(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = model.Lineas.Count;

            viewModel.IndexLinea = cantLineas;

            for (int i = 0; i < cantLineas; i++)
            {
                viewModel.Lineas.Add(new LineaOrden());
            }

            LineaOrden nueva = new LineaOrden();

            nueva.LineasInternasOrden = new List<LineaInternaOrden>();

            LineaInternaOrden primera = new LineaInternaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = DateTime.Now.Year;
            mesActual.MesNumero = DateTime.Now.AddMonths(1).Month;
            mesActual.MesNombre = this.GetMesNombre(mesActual.MesNumero);

            mesActual.Dias = new List<Dia>();

            int nroDias = DateTime.DaysInMonth(mesActual.MesAnio, mesActual.MesNumero);

            for (int i = 0; i < nroDias; i++)
            {
                Dia dia = new Dia();
                dia.DiaNumero = i + 1;

                DateTime fecha = new DateTime();
                string fechaString = dia.DiaNumero + "/" + mesActual.MesNumero + "/" + DateTime.Now.Year;
                DateTime.TryParseExact(fechaString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
                dia.DiaNombre = this.GetDiaNombre(fecha.DayOfWeek.ToString());

                dia.NroEmisiones = 0;
                dia.TotalDia = 0;

                mesActual.Dias.Add(dia);
            }

            primera.Mes = mesActual;
            nueva.LineasInternasOrden.Add(primera);

            viewModel.Lineas.Add(nueva);

            viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = viewModel.Lineas.Count - 1;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopiarUltimoMes(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = model.Lineas.Count;

            viewModel.IndexLinea = cantLineas;

            for (int i = 0; i < cantLineas; i++)
            {
                viewModel.Lineas.Add(new LineaOrden());
            }

            int cantLineasInternas = model.Lineas[cantLineas - 1].LineasInternasOrden.Count;

            LineaOrden nueva = new LineaOrden();

            for (int i = 0; i < cantLineasInternas; i++)
            {
                Mes mesActual = new Mes();

                LineaInternaOrden interna = new LineaInternaOrden();

                model.Lineas[cantLineas - 1].LineasInternasOrden.Add(new LineaInternaOrden());

                mesActual.MesAnio = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesAnio;
                mesActual.MesNumero = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesNumero;

                mesActual.MesNombre = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesNombre;

                mesActual.Dias = new List<Dia>();

                foreach (Dia d in model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.Dias)
                {
                    Dia dia = new Dia();
                    dia.DiaNumero = d.DiaNumero;
                    dia.DiaNombre = d.DiaNombre;
                    dia.NroEmisiones = d.NroEmisiones;
                    dia.TotalDia = d.TotalDia;

                    mesActual.Dias.Add(dia);
                }

                interna.Mes = mesActual;

                nueva.LineasInternasOrden.Add(interna);
            }

            viewModel.Lineas.Add(nueva);


            viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = viewModel.Lineas.Count - 1;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        [ValidateInput(false)]
        public FileResult createPdf(string GridHtml)
        {

            Orden_Create_Model model = new Orden_Create_Model();
            model.ListaCampanias = campaniaAplicationService.GetAll();
            var PrimerCampania = model.ListaCampanias.FirstOrDefault();
            var CampaniaConDependencias = campaniaAplicationService.Get(PrimerCampania.CampaniaId);
            model.ListaMateriales = CampaniaConDependencias.Materiales;
            model.ListaMedios = medioApplicationService.GetAll();
            model.ListaProgramas = programaApplicationService.GetProgramasByMedio(model.ListaMedios.FirstOrDefault().MedioId);
            model.ListaClientes = clienteApplicationService.GetClientes();
            model.Lineas[0].LineasInternasOrden[0].Programa = new Programa();
            model.Lineas[0].LineasInternasOrden[0].Programa.Nombre = "Cocinando con Knorr";
            model.Lineas[0].LineasInternasOrden[0].Material = new Material();
            model.Lineas[0].LineasInternasOrden[0].Material.Descripcion = "Material 40sqg";
            model.Medio = new Medio();
            model.Medio.Nombre = "Canal 10";
            model.Emision = DateTime.Now;
            model.Campania = new Campania();
            model.Campania.Nombre = "Todos por uno";
            model.NroOrden = ordenApplicationService.GetNroOrden();
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            // setting image 
            string imagePath = "C:/Trabajos/Publinter2/Publinter/Content/Images/Logo-Publinter-dark-2017-final.png";
            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(imagePath);
            tif.ScalePercent(24f);
            tif.SetAbsolutePosition(document.PageSize.Width - 36f - 140f,
            document.PageSize.Height - 75f);
            document.Add(tif);
            // fin imagenes
            //Titulo Orden
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Garamond", 20);
            iTextSharp.text.Font regularFont = FontFactory.GetFont("Arial", 14);

            Paragraph OrdenNumero = new Paragraph("Órden Nro. " + model.NroOrden.ToString(), titleFont);
            OrdenNumero.Alignment = Element.ALIGN_LEFT;
            document.Add(OrdenNumero);
            //fin titulo orden
            //Linea separadora titulo
            Paragraph pLineaSeparadora = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
         //   pLineaSeparadora.ExtraParagraphSpace =30;
            document.Add(pLineaSeparadora);
            //

            CreateTableMedioCampaniaEmision(document, model);

            document.Add(pLineaSeparadora);
            //Table
            CreateTable(document,model);
            //Agrego linea footer
            
            document.Add(pLineaSeparadora);
            //Linea separadora footer
            Paragraph InversionTotal = new Paragraph("INVERSIÓN TOTAL: $" + model.TotalOrden.ToString());
            InversionTotal.Alignment = Element.ALIGN_CENTER;
            document.Add(InversionTotal);
            ////Fecha
            //document.Add(new Paragraph("Fecha: " + DateTime.Now.ToString()));
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("abc.pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return new FileStreamResult(workStream, "application/pdf");
        }
        private string GetMesNombre(int mes)
        {
            string retorno = "";
            switch (mes)
            {
                case 1:
                    retorno = "Enero";
                    break;
                case 2:
                    retorno = "Febrero";
                    break;
                case 3:
                    retorno = "Marzo";
                    break;
                case 4:
                    retorno = "Abril";
                    break;
                case 5:
                    retorno = "Mayo";
                    break;
                case 6:
                    retorno = "Junio";
                    break;
                case 7:
                    retorno = "Julio";
                    break;
                case 8:
                    retorno = "Agosto";
                    break;
                case 9:
                    retorno = "Setiembre";
                    break;
                case 10:
                    retorno = "Octubre";
                    break;
                case 11:
                    retorno = "Noviembre";
                    break;
                case 12:
                    retorno = "Diciembre";
                    break;
            }

            return retorno;
        }

        private string GetDiaNombre(string dia)
        {
            string retorno = "";

            switch (dia)
            {
                case "Sunday":
                    retorno = "Domingo";
                    break;
                case "Monday":
                    retorno = "Lunes";
                    break;
                case "Tuesday":
                    retorno = "Martes";
                    break;
                case "Wednesday":
                    retorno = "Miércoles";
                    break;
                case "Thursday":
                    retorno = "Jueves";
                    break;
                case "Friday":
                    retorno = "Viernes";
                    break;
                case "Saturday":
                    retorno = "Sábado";
                    break;
            }

            return retorno;
        }

        private Document  CreateTable(Document document, Orden_Create_Model model)
        {
            //fuente
            var FontWhite = FontFactory.GetFont("Helvetica", 10, new BaseColor(Color.White));
            var FontGris = FontFactory.GetFont("Helvetica", 18, new BaseColor(Color.Gray));
            //
            for (var i = 0; i < model.Lineas.Count; i++)
            {
                // Ante de crear tabla iserto un parrafo para darle margen
                Paragraph PmargenTop = new Paragraph("");
                PmargenTop.PaddingTop = 30;
                document.Add(PmargenTop);
                PdfPTable table = new PdfPTable(33);
                table.WidthPercentage = 100; //table width to 100per
               
                float[] widths = new float[] { 80f, 80f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f };
                table.SetWidths(widths);
                // thead 2 primeras
                PdfPCell ColSpan2 = new PdfPCell(new Phrase());
                ColSpan2.Colspan = 2;
                ColSpan2.BorderColor = BaseColor.WHITE;
                table.AddCell(ColSpan2);
                // thead titulo mes
                Phrase ph1 = new Phrase();
                ph1.Add(new Chunk(Environment.NewLine));
                PdfPCell colSpan33= new PdfPCell(new Phrase(model.Lineas[i].LineasInternasOrden[0].Mes.MesNombre, FontGris));
                colSpan33.Colspan = 33;
                colSpan33.Border = 0;
                colSpan33.BorderWidth = 0;
                colSpan33.BorderColor = new BaseColor(Color.LightGray);
                colSpan33.BackgroundColor = new BaseColor(Color.LightGray);
                colSpan33.HorizontalAlignment = Element.ALIGN_CENTER;
                colSpan33.VerticalAlignment = Element.ALIGN_MIDDLE;
                colSpan33.PaddingBottom = 10;
                table.DefaultCell.BorderWidth = 0;
                table.AddCell(colSpan33);
                PdfPCell RowSpan2 = new PdfPCell(new Phrase("Ubicación", FontWhite));
                RowSpan2.Rowspan = 2;
                RowSpan2.PaddingTop = 8;
                RowSpan2.HorizontalAlignment = Element.ALIGN_CENTER;
                RowSpan2.VerticalAlignment = Element.ALIGN_CENTER;
                RowSpan2.BackgroundColor = new BaseColor(Color.Black);
                table.AddCell(RowSpan2);
                RowSpan2.Phrase = new Phrase("Materiales", FontWhite);
                RowSpan2.PaddingTop = 10;
                table.AddCell(RowSpan2);
                //
                PdfPCell simpleCel = new PdfPCell();
                simpleCel.UseVariableBorders = true;
                simpleCel.BorderColor = BaseColor.BLACK;
                simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                foreach (var dia in model.Lineas[i].LineasInternasOrden[i].Mes.Dias)
                {
                    simpleCel.Phrase = new Phrase(dia.DiaNombre[0].ToString());
                    simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    simpleCel.Phrase.Font.Size = 10;
                    table.AddCell(simpleCel);
                }
                //Completo el mes segun los dias
                if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count < 31)
                    simpleCel = new PdfPCell(new Phrase(""));
                {
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 30)
                    {

                        table.AddCell(simpleCel);
                    }
                    else if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 29)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 28)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                }
                //Completa con los numeros de dias
               
                foreach (var dia in model.Lineas[i].LineasInternasOrden[i].Mes.Dias)
                {
                    simpleCel.Phrase = new Phrase(dia.DiaNumero.ToString());
                    simpleCel.Phrase.Font.Size = 10;
                    simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(simpleCel);
                }
                //Completo el mes segun los dias
                if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count < 31)
                    simpleCel = new PdfPCell(new Phrase(""));
                {
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 30)
                    {

                        table.AddCell(simpleCel);
                    }
                    else if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 29)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 28)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                }

                //linea de inversione
                RowSpan2 = new PdfPCell(new Phrase(model.Lineas[i].LineasInternasOrden[i].Programa.Nombre));
                RowSpan2.Rowspan = 2;
                RowSpan2.PaddingTop = 3;
                RowSpan2.Phrase.Font.Size = 10;
                RowSpan2.PaddingBottom = 3;
               
                RowSpan2.HorizontalAlignment = Element.ALIGN_CENTER;
                RowSpan2.VerticalAlignment = Element.ALIGN_CENTER;
                RowSpan2.BackgroundColor = new iTextSharp.text.BaseColor(Color.White);            
                table.AddCell(RowSpan2);
                RowSpan2.Phrase = new Phrase(model.Lineas[i].LineasInternasOrden[i].Material.Descripcion);
                RowSpan2.Phrase.Font.Size = 10;
                table.AddCell(RowSpan2);
                //
                simpleCel = new PdfPCell();
                simpleCel.UseVariableBorders = true;
                simpleCel.BorderColor = BaseColor.BLACK;
                
                simpleCel.PaddingTop = 6;
                for (var d = 0; d < model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count; d++)
                {
                    simpleCel.Phrase = new Phrase(model.Lineas[i].LineasInternasOrden[i].Mes.Dias[d].NroEmisiones.ToString());
                    simpleCel.Phrase.Font.Size = 10;

                    simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(simpleCel);
                    
                }
                if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count < 31)
                    simpleCel = new PdfPCell(new Phrase(""));
                {
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 30)
                    {

                        table.AddCell(simpleCel);
                    }
                    else if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 29)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                    if (model.Lineas[i].LineasInternasOrden[i].Mes.Dias.Count == 28)
                    {
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                        table.AddCell(simpleCel);
                    }
                }
                //table.SpacingAfter = 5;
                table.SpacingBefore = 8;
                document.Add(table);
            }
            

            return document;
        }
        private Document CreateTableMedioCampaniaEmision(Document document, Orden_Create_Model model)
        {
          
            Phrase CampaniaPh = new Phrase("Campaña: " + model.Campania.Nombre);
            Phrase MedioPh = new Phrase("Medio: " + model.Medio.Nombre);
            Phrase EmisionPh = new Phrase("Emisión: " + model.Emision.ToString("dd/MM/yyyy"));
            PdfPTable TableMedioCampaniaEmision = new PdfPTable(3);
            TableMedioCampaniaEmision.WidthPercentage = 100;
            PdfPCell unacell = new PdfPCell(CampaniaPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_LEFT;
            TableMedioCampaniaEmision.AddCell(unacell);
            unacell = new PdfPCell(MedioPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_CENTER;
            TableMedioCampaniaEmision.AddCell(unacell);
            unacell = new PdfPCell(EmisionPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_RIGHT;
            TableMedioCampaniaEmision.AddCell(unacell);
            TableMedioCampaniaEmision.SpacingBefore = 10;

            document.Add(TableMedioCampaniaEmision);
            return document;
        }


    }
}