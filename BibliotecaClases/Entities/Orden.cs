﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Orden
    {
        [Key, Column("OrdenId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenId { get; set; }

        public int NroOrden { get; set; }

        public DateTime Emision { get; set; }

        /// <summary>
        /// Cliente Navigation
        /// </summary>
        public int CampaniaId { get; set; }

        [ForeignKey("CampaniaId")]
        public Campania Campania { get; set; }

        public virtual List<LineaOrden> LineasOrden { get; set; }

        public decimal Total { get; set; }

        /// <summary>
        /// Usuario Navigation
        /// </summary>
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }


        public int MedioId { get; set; }

        [ForeignKey("MedioId")]
        public Medio Medio { get; set; }
    }
}
