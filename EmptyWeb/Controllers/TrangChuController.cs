using EmptyWeb.Contexts;
using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    [TraceLog]
    public class TrangChuController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.danhSachBaiViet12 = EntityContext.BaiViet.Where(b => b.IsPinned && b.SortNumber < 3).OrderBy(b => b.SortNumber).ToList();
            ViewBag.danhSachBaiViet345 = EntityContext.BaiViet.Where(b => b.IsPinned && b.SortNumber < 6 && b.SortNumber > 2).OrderBy(b => b.SortNumber).ToList();
            ViewBag.danhSachBaiVietRecent = EntityContext.BaiViet.Where(b => !b.IsPinned && !b.IsHidden).OrderByDescending(b => b.NgayDang).ToList();
            return View();
        }

        public ActionResult GioiThieu()
        {
            object template = EntityContext.HtmlTemplate.FirstOrDefault(t => t.TemplateCode == PageEnums.Template.GioiThieu).Content;
            return View(template);
        }

        public ActionResult TestSite()
        {
            return View();
        }

        #region DangKyGiaSu
        public ActionResult DangKyLamGiaSu(string message)
        {
            return View(new DangKyGiaSu());
        }

        [HttpPost]
        public async Task<ActionResult> DangKyLamGiaSu(DangKyGiaSu model, HttpPostedFileBase fileAnhThe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileAnhThe != null)
                    {
                        using (var fileStream = new FileStream(Path.Combine(Server.MapPath("~/Content/img"), "anh-the.jpeg"), FileMode.Create))
                        {
                            fileAnhThe.InputStream.CopyTo(fileStream);
                        }
                        var imgurResult = await ImgurService.UploadImage(System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Content/img"), "anh-the.jpeg")));
                        if (imgurResult != null)
                        {
                            model.AnhThe = imgurResult.Link;
                        }
                    }
                    EntityContext.DangKyGiaSu.Add(model);
                    EntityContext.SaveChanges();
                    return OK();
                }
                catch (Exception e)
                {
                    LogContext.Record(e.Message);
                }
                return Error();
            }
            else
            {
                return Error(ModelState);
            }
        }
        #endregion

        #region TimGiaSu
        public ActionResult YeuCauTimGiaSu()
        {
            return View(new TimGiaSu());
        }

        [HttpPost]
        public async Task<ActionResult> YeuCauTimGiaSu(TimGiaSu model)
        {
            if (ModelState.IsValid)
            {
                EntityContext.TimGiaSu.Add(model);
                EntityContext.SaveChanges();
                return OK();
            }
            return Error(ModelState);
        }
        #endregion

        #region ChuyenMuc
        public ActionResult ChuyenMuc(string id)
        {
            var cm = EntityContext.ChuyenMuc.FirstOrDefault(r => r.Url == id && !r.IsHidden);
            if (cm != null)
            {
                return View("ChiTietChuyenMuc", cm);
            }
            return HttpNotFound();
        }
        #endregion

        #region BaiViet
        public ActionResult BaiViet(string id)
        {
            var bv = EntityContext.BaiViet.FirstOrDefault(r => r.UrlBaiViet == id && !r.IsHidden);
            if (bv != null)
            {
                bv.ReadCount++;
                EntityContext.SaveObject(bv);
                return View("ChiTietBaiViet", bv);
            }
            return HttpNotFound();
        }
        #endregion

    }
}