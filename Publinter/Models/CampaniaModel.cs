using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publinter.Models
{
    public class CampaniaModel: PublinteController
    {

        public int CampaniaId { get; set; }
        public string Nombre { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int AnuncianteId { get; set; }
        public Anunciante Anunciante { get; set; }
        public int MedioId { get; set; }

        public List<Material> Materiales { get; set; }
        public List<Get_Cliente_Data> ListaClientes { get; set; }
        public List<Get_Anunciante_Data> ListaDeAnunciante { get; set; }
        //public List<Material> ListaDeMaterialesToSelect { get; set; }
    }
}