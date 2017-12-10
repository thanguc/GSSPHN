using EmptyWeb.Contexts;
using EmptyWeb.Services;
using EmptyWeb.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly EntityContext EntityContext;
        protected readonly LogContext LogContext;
        protected readonly ImgurService ImgurService;
        protected readonly IdentityContext IdentityContext;

        public BaseController()
        {
            LogContext = new LogContext();
            EntityContext = new EntityContext();
            IdentityContext = new IdentityContext();
            ImgurService = new ImgurService(LogContext);
        }

        protected void Alert(PageEnums.AlertMessage alert)
        {
            ViewBag.AlertMessage = alert;
        }

        protected ActionResult OK()
        {
            return new HttpStatusCodeResult(200);
        }

        protected ActionResult OK(object obj)
        {
            return Json(obj);
        }

        protected ActionResult Error()
        {
            return new HttpStatusCodeResult(500);
        }

        protected ActionResult Error(string messages)
        {
            return Json(new { Error = messages });
        }

        protected ActionResult Error(IEnumerable<string> messages)
        {
            return Json(new { Error = messages });
        }

        protected ActionResult Error(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { Error = errors });
        }

        protected ActionResult Error(EntityContext entityContext)
        {
            var errors = entityContext.GetValidationErrors().SelectMany(v => v.ValidationErrors).Select(e => e.ErrorMessage);
            return Json(new { Error = errors });
        }
    }
}