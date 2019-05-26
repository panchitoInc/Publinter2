using DataModule;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class ConfiguracionEmailRepository: PublinterRepository, IConfiguracionEmailRepository
    {
        public ConfiguracionEmailRepository(AppUser context) : base(context)
        {

        }
        public ConfiguracionEmail Get()
        {
            ConfiguracionEmail conf;
            using (var context = new PublinterContext())
            {
                conf = context.ConfiguracionEmail.OrderByDescending(c => c.ConfiguracionEmailId).FirstOrDefault(); ;
            }
            return conf;
        }

        public bool Update(ConfiguracionEmail model)
        {
            try
            {
                
                using (var context = new PublinterContext())
                {
                    //buscado = context.ConfiguracionEmail.Last();
                    //if (buscado == null) { }
                    //buscado.Host = model.Host;
                    //buscado.Port = model.Port;
                    //buscado.UserName = model.UserName;
                    //buscado.Password = model.Password;
                    model.FechaAlta = DateTime.Now;
                    context.ConfiguracionEmail.Add(model);
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }

                return true;
            }
            catch 
            {
                return false;
            }
            
        }
    }
}
