namespace Mvc
{
    using System.Web.Mvc;

    public class PublinterApplicationContextFilter : ActionFilterAttribute
    {
        internal class Http403Result : ActionResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                // Set the response code to 403.
                context.HttpContext.Response.StatusCode = 403;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {           
           /* var appuser = new AppUser(filterContext.HttpContext.User as ClaimsPrincipal);            
            var area = (filterContext.RequestContext.RouteData.DataTokens["area"] ?? "").ToString().ToLower();
            var tienepermiso = String.IsNullOrEmpty(area); //Si no accede a un area, tiene acceso permitido       

            if ((appuser.TengoUsuarioId)&&(!tienepermiso))
            {
                /* tienepermiso = filterContext.HttpContext.Request.IsAjaxRequest(); //Si tiene usuario y es un ajax puede pasar. :s

                 if (!tienepermiso)
                 {*/
                //Si tiene area, tiene UsuarioId y no tienepermiso aún. 

               /* var data = appuser.UserAccess;
                    var lista = JsonConvert.DeserializeObject<List<Get_user_access>>(data);

                    var action = (filterContext.RequestContext.RouteData.Values["action"] ?? "").ToString().ToLower();
                    var controller = (filterContext.RequestContext.RouteData.Values["controller"] ?? "").ToString().ToLower();

                    tienepermiso = tienepermiso || lista.Exists(x => x.Area.ToLower().Equals(area) && x.Controller.ToLower().Equals(controller)); // && x.ActionName.ToLower().Equals(action));
               // }
            }



            if (tienepermiso)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new Http403Result();
            }*/
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}
