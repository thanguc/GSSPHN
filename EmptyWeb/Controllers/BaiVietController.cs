using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class BaiVietController : BaseController
    {
        public ActionResult Index(string url)
        {
            var bv = EntityContext.BaiViet.FirstOrDefault(r => r.UrlBaiViet == url && !r.IsHidden);
            if (bv != null)
            {
                bv.ReadCount++;
                EntityContext.SaveObject(bv);
                return View("ChiTietBaiViet", bv);
            }
            return HttpNotFound();
        }
    }
}