using BibliotecaClases.Entities;
using DataModule;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class MedioRepository : PublinterRepository, IMedioRepository 
    {

        public MedioRepository(AppUser Context) : base(Context)
        {

        }



        public List<Medio> GetAll()
        {

            var Medios = new List<Medio>();

            using (var context = new PublinterContext())
            {
                Medios = context.Medio.ToList();
            }

            return Medios.ToList();
        }
        public Medio Get(int id)
        {
            var med = new Medio();

            using (var context = new PublinterContext())
            {
                med = context.Medio.FirstOrDefault(x => x.MedioId == id);
            }

            return med;
        }
    }
}
