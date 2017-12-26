using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class ChuyenMucController : BaseController
    {
        public ActionResult ChuyenMuc(string url)
        {
            var cm = EntityContext.ChuyenMuc.FirstOrDefault(r => r.Url == url && !r.IsHidden);
            if (cm != null)
            {
                return View("ChiTietChuyenMuc", cm);
            }
            return HttpNotFound();
        }
    }
}