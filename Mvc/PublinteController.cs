using System.IO;
using System.Security.Claims;
using System.Web.Mvc;

namespace Mvc
{
    public abstract class PublinteController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(User as ClaimsPrincipal);
            }
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected ActionResult SelectView(string viewName, object model, string outputType = "html")
        {
            if (outputType.ToLower() == "json")
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MobileViewName = "Mobile/" + viewName;
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, MobileViewName);

                if ((Request.Browser.IsMobileDevice) && ((viewResult != null) && (viewResult.View != null)))
                {
                    return View(MobileViewName, model);
                }
                else
                {
                    return View(viewName, model);
                }
            }
        }
    }
}
