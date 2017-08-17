using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class TrangChuController : BaseController
    {
        public ActionResult Index()
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
            try
            {
                //var imgurResult = await Imgur.UploadImage(fileAnhThe.InputStream);
                //if (imgurResult != null)
                //{
                    //model.AnhThe = imgurResult.Link;
                    model.ID = Guid.NewGuid().ToString();
                    model.TrangThai = TrangThaiDangKy.Submitted;
                    model.NgayTao = DateTime.Now;
                    BaseContext.DangKyGiaSu.Add(model);
                    await BaseContext.SaveChangesAsync();
                //}
                Alert(AlertMessage.DangKyLamGiaSuThanhCong);
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
                Alert(AlertMessage.DangKyLamGiaSuThatBai);
            }
            return View(model);
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
            try
            {
                model.ID = Guid.NewGuid().ToString();
                model.TrangThai = TrangThaiYeuCau.Submitted;
                model.NgayTao = DateTime.Now;
                BaseContext.TimGiaSu.Add(model);
                await BaseContext.SaveChangesAsync();
                Alert(AlertMessage.DangKyTimGiaSuThanhCong);
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
                Alert(AlertMessage.DangKyTimGiaSuThatBai);
            }
            return View(model);
        }
        #endregion
    }
}