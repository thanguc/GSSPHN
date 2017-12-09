using EmptyWeb.Data;
using EmptyWeb.Services;
using EmptyWeb.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AppDbContext DBContext;
        protected readonly LoggingService Logger;
        protected readonly ImgurService Imgur;

        public BaseController()
        {
            DBContext = new AppDbContext();
            Logger = new LoggingService(DBContext);
            Imgur = new ImgurService(DBContext, Logger);
        }

        protected void Alert(AlertMessage alert)
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

        protected ActionResult Error(AppDbContext baseContext)
        {
            var errors = baseContext.GetValidationErrors().SelectMany(v => v.ValidationErrors).Select(e => e.ErrorMessage);
            return Json(new { Error = errors });
        }
    }
}