﻿using DataModule;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class MaterialRepository : PublinterRepository, IMaterialRepository
    {
        public MaterialRepository(AppUser Context) : base(Context)
        {

        }

        public int Create(Material mat)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    Cliente cli = context.Cliente.Include("Materiales").FirstOrDefault(x => x.ClienteId.Equals(mat.ClienteId));
                    cli.Materiales.Add(mat);

                    context.Entry(mat).State = System.Data.Entity.EntityState.Added;
                    context.Entry(cli).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    return mat.MaterialId;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IList<Material> GetAll()
        {
            IEnumerable<Material> Lista;

            using (var context = new PublinterContext())
            {
                Lista = context.Material
                    .Include("TipoMaterial").ToList();
            }
            return Lista.ToList();
        }

        public IList<TipoMaterial> GetTipos()
        {
            IEnumerable<TipoMaterial> retorno;

            using (var context = new PublinterContext())
            {
                retorno = context.TipoMaterial.ToList();
            }
            return retorno.ToList();
        }

        public void Update(Material mat)
        {
            throw new NotImplementedException();
        }
    }
}
