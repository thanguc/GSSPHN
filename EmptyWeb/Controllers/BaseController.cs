using EmptyWeb.Contexts;
using EmptyWeb.Services;
using EmptyWeb.Shared;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly EntityContext EntityContext;
        protected readonly LoggingService Logger;
        protected readonly ImgurService Imgur;
        protected readonly IdentityContext IdentityContext;

        public BaseController()
        {
            EntityContext = new EntityContext();
            Logger = new LoggingService(EntityContext);
            Imgur = new ImgurService(EntityContext, Logger);
            IdentityContext = new IdentityContext();
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