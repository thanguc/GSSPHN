using EmptyWeb.Contexts;
using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    [Authorize]
    public class QuanLyController : BaseController
    {

        // GET: QuanLy
        public ActionResult Index()
        {
            return View();
        }

        #region Report
        [HttpPost]
        public ActionResult GetReportData()
        {
            var wantToBeTutorCount = EntityContext.DangKyGiaSu.Count(d => d.TrangThai == PageEnums.TrangThaiDangKy.Submitted);
            var lookForTutorCount = EntityContext.TimGiaSu.Count(d => d.TrangThai == PageEnums.TrangThaiYeuCau.Submitted);
            var feedbackCount = 0;
            var result = new
            {
                wantToBeTutorCount = wantToBeTutorCount,
                lookForTutorCount = lookForTutorCount,
                feedbackCount = feedbackCount
            };
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetAllDangKyLamGiaSu()
        {
            var submitting = EntityContext.DangKyGiaSu.Include(d => d.QueQuan).Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_WantToBeTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllYeuCauTimGiaSu()
        {
            var submitting = EntityContext.TimGiaSu.Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_LookingForTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllLog()
        {
            return PartialView("_SystemLogs", LogContext.Logs.OrderByDescending(l => l.Timestamp).ToList());
        }
        #endregion

        #region Content

        public ActionResult Muc()
        {
            var result = EntityContext.Muc.ToList();
            return PartialView("_Muc", result);
        }

        [TraceLog]
        public ActionResult ThemMuc(Muc model)
        {
            if (ModelState.IsValid)
            {
                model.SortNumber = EntityContext.Muc.Count() + 1;
                EntityContext.Muc.Add(model);
                EntityContext.SaveChanges();
                return OK();
            }
            return Error(ModelState);
        }

        [TraceLog]
        public ActionResult UpdateTieuDeMuc(Guid pk, string value)
        {
            var muc = EntityContext.Muc.Find(pk);
            muc.TieuDe = value;
            if (EntityContext.IsValid)
            {
                EntityContext.SaveObject(muc);
                return OK(value);
            }
            else
            {
                return Error(EntityContext);
            }
        }

        [TraceLog]
        public ActionResult AddEditChuyenMuc(ChuyenMuc model)
        {
            ChuyenMuc cm = EntityContext.ChuyenMuc.Find(model.ChuyenMucId);
            if (cm == null)
            {
                model.SortNumber = EntityContext.ChuyenMuc.Count(c => c.MucId == model.MucId) + 1;
                EntityContext.ChuyenMuc.Add(model);
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveChanges();
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            else
            {
                cm.IsHidden = model.IsHidden;
                cm.Url = model.Url;
                cm.TieuDe = model.TieuDe;
                cm.NoiDung = model.NoiDung;
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveObject(cm);
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            return OK();
        }

        public ActionResult GetSoLuongChuyenMuc(Guid id)
        {
            return Json(EntityContext.Muc.Find(id).ChuyenMucs.Count);
        }

        public ActionResult ChiTietMuc(Guid id)
        {
            var result = EntityContext.Muc.Find(id);
            return PartialView("_ChiTietMuc", result);
        }

        [TraceLog]
        public ActionResult SwitchMucAnHien(Guid id, bool status)
        {
            var muc = EntityContext.Muc.Find(id);
            muc.IsHidden = !status;
            EntityContext.SaveObject(muc);
            return OK();
        }

        [TraceLog]
        public ActionResult SwitchChuyenMucAnHien(Guid id, bool status)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            chuyenMuc.IsHidden = !status;
            EntityContext.SaveObject(chuyenMuc);
            return OK();
        }

        [TraceLog]
        public ActionResult XoaMuc(Guid id)
        {
            var muc = EntityContext.Muc.Find(id);
            var tieude = muc.TieuDe;
            var nextMucs = EntityContext.Muc.Where(m => m.SortNumber > muc.SortNumber);
            foreach (var m in nextMucs)
            {
                m.SortNumber--;
            }
            EntityContext.ChuyenMuc.RemoveRange(muc.ChuyenMucs);
            EntityContext.DeleteObject(muc);
            return OK(tieude);
        }

        [TraceLog]
        public ActionResult XoaChuyenMuc(Guid id)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            var tieude = chuyenMuc.TieuDe;
            var nextChuyenMucs = EntityContext.ChuyenMuc.Where(m => m.SortNumber > chuyenMuc.SortNumber);
            foreach (var m in nextChuyenMucs)
            {
                m.SortNumber--;
            }
            if (EntityContext.IsValid)
            {
                EntityContext.DeleteObject(chuyenMuc);
            }
            else
            {
                return Error(EntityContext);
            }
            return OK(tieude);
        }

        [TraceLog]
        public ActionResult MoveMuc(Guid id, int value)
        {
            var muc = EntityContext.Muc.Find(id);
            if (value == -1 && muc.SortNumber > 1)
            {
                var preMuc = EntityContext.Muc.Single(c => c.SortNumber == muc.SortNumber - 1);
                muc.SortNumber--;
                preMuc.SortNumber++;
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveObject(muc, preMuc);
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            else if (value == 1 && muc.SortNumber < EntityContext.Muc.Count())
            {
                var nextMuc = EntityContext.Muc.Single(c => c.SortNumber == muc.SortNumber + 1);
                muc.SortNumber++;
                nextMuc.SortNumber--;
                EntityContext.SaveObject(muc, nextMuc);
            }
            return OK();
        }

        [TraceLog]
        public ActionResult MoveChuyenMuc(Guid id, int value)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            if (value == -1 && chuyenMuc.SortNumber > 1)
            {
                var preChuyenMuc = EntityContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber - 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber--;
                preChuyenMuc.SortNumber++;
                EntityContext.SaveObject(chuyenMuc, preChuyenMuc);
            }
            else if (value == 1 && chuyenMuc.SortNumber < EntityContext.ChuyenMuc.Count(c => c.MucId == chuyenMuc.MucId))
            {
                var nextChuyenMuc = EntityContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber + 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber++;
                nextChuyenMuc.SortNumber--;
                EntityContext.SaveObject(chuyenMuc, nextChuyenMuc);
            }
            return OK();
        }

        public ActionResult GetChuyenMucModal(Guid? id)
        {
            return PartialView("_ModalThemChuyenMuc", id.HasValue ? EntityContext.ChuyenMuc.Find(id) : null);
        }

        public ActionResult _BaiViet()
        {
            var result = new List<BaiViet>();
            result.AddRange(EntityContext.BaiViet.Where(b => b.IsPinned).OrderBy(b => b.SortNumber).ToList());
            result.AddRange(EntityContext.BaiViet.Where(b => !b.IsPinned).OrderByDescending(b => b.SortNumber).ToList());
            return PartialView("_BaiViet/_BaiViet", result);
        }

        [TraceLog]
        [HttpPost]
        public ActionResult ThemBaiViet(BaiViet model)
        {
            model.SortNumber = EntityContext.BaiViet.Count() + 1;
            EntityContext.BaiViet.Add(model);
            if (EntityContext.IsValid)
            {
                EntityContext.SaveChanges();
                return OK();
            }
            return Error(EntityContext);
        }

        [TraceLog]
        [HttpPost]
        public ActionResult SuaBaiViet(BaiViet model)
        {
            var bv = EntityContext.BaiViet.Find(model.BaiVietId);
            if (bv != null)
            {
                bv.NoiDung = model.NoiDung;
                bv.TieuDe = model.TieuDe;
                bv.UrlAnhBia = model.UrlAnhBia;
                bv.UrlBaiViet = model.UrlBaiViet;
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveObject(bv);
                    return OK();
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            return Error();
        }

        [TraceLog]
        public ActionResult SwitchBaiVietAnHien(Guid id, bool status)
        {
            var bv = EntityContext.BaiViet.Find(id);
            bv.IsHidden = !status;
            if (EntityContext.IsValid)
            {
                EntityContext.SaveObject(bv);
                return OK();
            }
            return Error(EntityContext);
        }

        [TraceLog]
        [HttpPost]
        public ActionResult GhimBaiViet(Guid id)
        {
            var bv = EntityContext.BaiViet.Find(id);
            if (bv != null && !bv.IsPinned)
            {
                if (EntityContext.BaiViet.Count(b => b.IsPinned && b.BaiVietId != id) == 5)
                {
                    return Error("Số bài viết được ghim đã đạt tối đa 5 bài, hãy bỏ ghim 1 bài viết trước");
                }
                else
                {
                    var previouses = EntityContext.BaiViet.Where(b => b.SortNumber < bv.SortNumber);
                    foreach (var pre in previouses)
                    {
                        pre.SortNumber++;
                    }
                    bv.SortNumber = 1;
                    bv.IsPinned = true;
                    if (EntityContext.IsValid)
                    {
                        EntityContext.SaveChanges();
                        return OK();
                    }
                    return Error(EntityContext);
                }
            }
            return Error();
        }

        [TraceLog]
        [HttpPost]
        public ActionResult BoGhimBaiViet(Guid id)
        {
            var bv = EntityContext.BaiViet.Find(id);
            if (bv != null && bv.IsPinned)
            {
                var nexts = EntityContext.BaiViet.Where(b => b.SortNumber > bv.SortNumber && (b.IsPinned || (!b.IsPinned && b.NgayDang < bv.NgayDang)));
                foreach (var next in nexts)
                {
                    next.SortNumber--;
                }
                bv.SortNumber = EntityContext.BaiViet.Where(b => b.IsPinned || (!b.IsPinned && b.NgayDang < bv.NgayDang)).Count();
                bv.IsPinned = false;
                EntityContext.SaveChanges();
                return OK();
            }
            return Error();
        }

        [TraceLog]
        [HttpPost]
        public ActionResult SwapBaiViet(Guid idFrom, Guid idTo)
        {
            var bvFrom = EntityContext.BaiViet.Find(idFrom);
            var bvTo = EntityContext.BaiViet.Find(idTo);
            if (bvFrom != null && bvTo != null && bvFrom.IsPinned && bvTo.IsPinned)
            {
                var tmp = bvFrom.SortNumber;
                bvFrom.SortNumber = bvTo.SortNumber;
                bvTo.SortNumber = tmp;
                EntityContext.SaveObject(bvFrom, bvTo);
                return OK();
            }
            return Error();
        }

        [TraceLog]
        public ActionResult XoaBaiViet(Guid id)
        {
            var bv = EntityContext.BaiViet.Find(id);
            if (bv.IsPinned)
            {
                return Error("Không được xóa bài viết đang ghim");
            }
            if (bv != null)
            {
                var tieude = bv.TieuDe;
                var nextBaiViets = EntityContext.BaiViet.Where(m => m.SortNumber > bv.SortNumber);
                foreach (var m in nextBaiViets)
                {
                    m.SortNumber--;
                }
                if (EntityContext.IsValid)
                {
                    EntityContext.DeleteObject(bv);
                    return OK(tieude);
                }
                return Error(EntityContext);
            }
            return Error();
        }

        [TraceLog]
        public ActionResult ResetThuTuBaiViet()
        {
            var allBaiViets = EntityContext.BaiViet.OrderBy(b => b.NgayDang);
            int i = 1;
            foreach (var m in allBaiViets)
            {
                m.IsPinned = false;
                m.IsHidden = false;
                m.SortNumber = i;
                i++;
            }
            if (EntityContext.IsValid)
            {
                EntityContext.SaveChanges();
                return OK();
            }
            return Error(EntityContext);
        }

        public ActionResult _GioiThieu()
        {
            var template = EntityContext.HtmlTemplate.FirstOrDefault(t => t.TemplateCode == PageEnums.Template.GioiThieu);
            return PartialView("_GioiThieu", template.Content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveGioiThieu(string content)
        {
            var template = EntityContext.HtmlTemplate.FirstOrDefault(t => t.TemplateCode == PageEnums.Template.GioiThieu);
            template.Content = content;
            EntityContext.SaveObject(template);
            return OK();
        }

        public ActionResult _DieuKhoan()
        {
            var template = EntityContext.HtmlTemplate.Where(t => t.TemplateCode == PageEnums.Template.DieuKhoanGiaSu || t.TemplateCode == PageEnums.Template.DieuKhoanPhuHuynh);
            return PartialView("_DieuKhoan", template.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveDieuKhoanGiaSu(string content)
        {
            var template = EntityContext.HtmlTemplate.FirstOrDefault(t => t.TemplateCode == PageEnums.Template.DieuKhoanGiaSu);
            template.Content = content;
            EntityContext.SaveObject(template);
            return OK();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveDieuKhoanPhuHuynh(string content)
        {
            var template = EntityContext.HtmlTemplate.FirstOrDefault(t => t.TemplateCode == PageEnums.Template.DieuKhoanPhuHuynh);
            template.Content = content;
            EntityContext.SaveObject(template);
            return OK();
        }
        #endregion

        #region Account
        public ActionResult _TaiKhoan()
        {
            var accounts = IdentityContext.Users;
            return PartialView("_TaiKhoan/_TaiKhoan", accounts.ToList());
        }
        #endregion

    }
}