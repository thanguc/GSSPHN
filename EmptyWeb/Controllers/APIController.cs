using EmptyWeb.Contexts;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    [TraceLog]
    public class APIController : BaseController
    {
        private readonly EntityContext _db = new EntityContext();

        public ActionResult Imgur()
        {
            return OK();
        }
    }
}