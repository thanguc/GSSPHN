using EmptyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace EmptyWeb.Contexts
{
    public class LogContext : DbContext
    {
        public LogContext() : base("LogContext")
        {
        }

        public DbSet<Log> Logs { get; set; }

        public void Record(Log log)
        {
            this.Logs.Add(log);
            this.SaveChangesAsync();
        }

        public void Record(string message)
        {
            this.Logs.Add(new Log { Message = message });
            this.SaveChangesAsync();
        }
    }

    public class TraceLog : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                Exception exception = exceptionContext.Exception;
                var request = exceptionContext.HttpContext.Request;
                Log log = new Log
                {
                    Username = request.IsAuthenticated ? exceptionContext.HttpContext.User.Identity.Name : "Anonymous",
                    IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                    Browser = request.Browser.Type,
                    UrlAccessed = request.RawUrl,
                    Message = SerializeData(request.Form) + string.Format("\nException: {0}\nSource: {1}\nStackTrace: {2}", exception.Message, exception.Source, exception.StackTrace)
                };
                using (LogContext logContext = new LogContext())
                {
                    logContext.Record(log);
                }
                if (exceptionContext.HttpContext.Request.IsAjaxRequest())
                {
                    exceptionContext.HttpContext.Response.StatusCode = 500;
                    exceptionContext.Result = new EmptyResult();
                }
                exceptionContext.Result = new RedirectResult("/Shared/Error");
                exceptionContext.ExceptionHandled = true;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            Log log = new Log
            {
                Username = request.IsAuthenticated ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Browser = request.Browser.Type,
                UrlAccessed = request.RawUrl,
                Message = SerializeData(request.Form)
            };

            using (LogContext logContext = new LogContext())
            {
                logContext.Record(log);
            }

            base.OnActionExecuting(filterContext);
        }

        private string SerializeData(NameValueCollection form)
        {
            var dictionary = new Dictionary<string, object>();
            try
            {
                form.CopyTo(dictionary);
            }
            catch (Exception)
            {
            }
            var content = new string[dictionary.Count];
            var index = 0;
            foreach (var item in dictionary)
            {
                content[index] = string.Format("{0}:{1}", item.Key, item.Value);
                index++;
            }
            return "Data: { " + string.Join(",", content) + " }";
        }
    }
}
