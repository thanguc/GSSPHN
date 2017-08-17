using EmptyWeb.Data;
using EmptyWeb.Services;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AppDbContext BaseContext;
        protected readonly LoggingService Logger;
        protected readonly ImgurService Imgur;

        public BaseController()
        {
            BaseContext = new AppDbContext();
            Logger = new LoggingService(BaseContext);
            Imgur = new ImgurService(BaseContext, Logger);
        }

        protected void Alert(AlertMessage alert)
        {
            ViewBag.AlertMessage = alert;
        }
    }
}