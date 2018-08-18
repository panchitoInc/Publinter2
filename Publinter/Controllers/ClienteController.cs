using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;

namespace Publinter.Controllers
{
    public class ClienteController : PublinteController
    {
        IClienteApplicationService _clienteApplicationService;

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

        public IList<Cliente> GetClientes()
        {
            return clienteApplicationService.GetClientes();
        }
    }
}