﻿using System.Data.Entity;
using DataModule.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.UI.WebControls;
using System;
using DataModule.Entities;

namespace DataModule
{
    public partial class PublinterContext : DbContext
    {
        public PublinterContext() : base("name=PublinterContext")
        {
            Database.SetInitializer<PublinterContext>(new CreateDatabaseIfNotExists<PublinterContext>());
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PublinterContext, Configuration>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 8));

        }

        public DbSet<Anunciante> Anunciante { get; set; }
        public DbSet<Campania> Campania { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ConfiguracionEmail> ConfiguracionEmail { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<Dia> Dia { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Mes> Mes { get; set; }
        public DbSet<Medio> Medio { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Programa> Programa { get; set; }
        public DbSet<Orden> Orden { get; set; }
        public DbSet<OrdenDeCompra> OrdenDeCompra { get; set; }
        public DbSet<TipoMaterial> TipoMaterial { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioAccess> UsuarioAccess { get; set; }
        public DbSet<TipoMedio> TipoMedio { get; set; }

    }
}
