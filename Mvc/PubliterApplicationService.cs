namespace Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PublinterApplicationService
    {
        public AppUser Context;
        public PublinterApplicationService(AppUser context)
        {
            this.Context = context;
        }


    }
}
