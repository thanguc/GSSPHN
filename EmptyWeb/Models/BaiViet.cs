using EmptyWeb.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace EmptyWeb.Models
{
    public class BaiViet : IValidatableObject
    {
        public Guid BaiVietId { get; set; } = Guid.NewGuid();
        public string TieuDe { get; set; }
        [AllowHtml]
        public string NoiDung { get; set; }
        public string UrlBaiViet { get; set; }
        public string UrlAnhBia { get; set; }
        public DateTime NgayDang { get; set; } = DateTime.Now;
        public int SortNumber { get; set; }
        public bool IsPinned { get; set; } = false;
        public bool IsHidden { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new EntityContext())
            {
                if (db.BaiViet.Any(c => c.TieuDe == this.TieuDe && c.BaiVietId != this.BaiVietId))
                    yield return new ValidationResult("Tiêu đề chuyên mục đã tồn tại", new[] { "TieuDe" });
                if (db.BaiViet.Any(c => c.UrlBaiViet == this.UrlBaiViet && c.BaiVietId != this.BaiVietId))
                    yield return new ValidationResult("URL chuyên mục đã tồn tại", new[] { "Url" });
                if (IsHidden && IsPinned)
                    yield return new ValidationResult("Không thể ẩn bài viết được ghim hoặc ghim bài viết bị ẩn", new[] { "IsPinned", "IsHidden" });
            }
        }
    }
}
