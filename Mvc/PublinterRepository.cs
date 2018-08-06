using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc
{
 
    public class PublinterRepository
    {
        public AppUser Context;

        public PublinterRepository(AppUser context)
        {
            this.Context = context;
        }


    }
}
