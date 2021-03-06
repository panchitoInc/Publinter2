﻿using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using Publinter.Extensions;
using System.Linq;
using System.Web;
using DataModule.EntitiesResult;

namespace Publinter.Models
{
    public class Orden_Create_Model
    {
        public Orden_Create_Model()
        {
            this.Emision = DateTime.Now.Date;
            this.ListaProgramas = new List<Get_Programa_Data>();
            this.ListaMateriales = new List<Material>();
            this.ListaClientes = new List<Get_Cliente_Data>();
            this.ListaAnunciantes = new List<Get_Anunciante_Data>();
            this.ListaCampanias = new List<Get_all_campania>();
            this.PorcentajeBonificado = 0;

            this.Lineas = new List<LineaOrden>();

            LineaOrden nueva = new LineaOrden();
       
            Mes mesActual = new Mes();

            mesActual.MesNumero = DateTime.Now.Month;
            mesActual.MesAnio = DateTime.Now.Year;
            mesActual.MesNombre = this.GetMesNombre(mesActual.MesNumero);

            mesActual.Dias = new List<Dia>();

            int nroDias = DateTime.DaysInMonth(DateTime.Now.AddMonths(1).Year, mesActual.MesNumero);

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

            nueva.LineasInternasOrden = new List<LineaInternaOrden>();

            LineaInternaOrden nuevali = new LineaInternaOrden();
            nuevali.Mes = mesActual;

            nuevali.LineaBonificada = new LineaBonificada();
            nuevali.LineaBonificada.Mes = mesActual;

            nueva.LineasInternasOrden.Add(nuevali);

            this.Lineas.Add(nueva);
        }

        public int OrdenId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Emision { get; set; }

        public int NroOrden { get; set; }

        public int AnuncianteId { get; set; }

        public Anunciante Anunciante { get; set; }

        public int CampaniaId { get; set; }

        public int MedioId { get; set; }

        public Medio Medio { get; set; }

        public Campania Campania { get; set; }

        public List<LineaOrden> Lineas { get; set; }

        public decimal TotalOrden { get; set; }

        public decimal TotalOrdenSegundos { get; set; }

        public int OrdenAnulada { get; set; }

        public int OrdenDeCompraId { get; set; }

        /// <summary>
        /// Para agregar una linea interna se pone aqui a que nro de linea se le agrega
        /// </summary>
        public int IndexLineaParaAgregar { get; set; }

        public int IndexLineaInternaParaAgregar { get; set; }

        public decimal PorcentajeBonificado { get; set; }

        public List<Medio> ListaMedios { get; set; }

        public List<Get_Anunciante_Data> ListaAnunciantes { get; set; }

        public List<Get_Cliente_Data> ListaClientes { get; set; }

        public List<Get_all_campania> ListaCampanias { get; set; }

        public List<Get_Programa_Data> ListaProgramas { get; set; }

        public List<Material> ListaMateriales { get; set; }

        /// <summary>
        /// 0 guardar
        /// 1 Descargar y guardar
        /// 2 Enviar y guardar
        /// </summary>
        public int GuardarEnviarDescargar { get; set; }

        public Orden ToOrden()
        {
            Orden orden = new Orden
            {
                Emision = this.Emision,
                NroOrden = this.NroOrden,
                CampaniaId = this.CampaniaId,
                TotalSegundos = this.TotalOrdenSegundos,
                Total = this.TotalOrden,
                UsuarioId = this.UsuarioId,
                MedioId = this.MedioId
            };
            //email
            if(this.Email != null)
            {
                this.Email.FechaEnviado = DateTime.Now;
                orden.Emails.Add(this.Email);
            }
            
            orden.MedioId = this.MedioId;

            if (this.OrdenDeCompraId == 0)
                orden.OrdenDeCompraId = null;
            else
                orden.OrdenDeCompraId = this.OrdenDeCompraId;

            foreach (LineaOrden l in this.Lineas)
            {
                l.LineasInternasOrden = l.LineasInternasOrden.Where(x => !x.Deleted).ToList();
            }

            orden.LineasOrden = this.Lineas;
            
            if (this.OrdenAnulada != 0) {
                orden.AnulaA = this.OrdenAnulada;
            }

            return orden;
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

            switch (dia) {
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
        public Email Email { get; set; }
        
    }
}