using EmptyWeb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Models
{
    public class ChuyenMuc : IValidatableObject
    {
        public Guid ChuyenMucId { get; set; } = Guid.NewGuid();
        public string TieuDe { get; set; }
        [AllowHtml]
        public string NoiDung { get; set; }
        public bool IsHidden { get; set; }
        public string Url { get; set; }
        public int? SortNumber { get; set; }
        public Guid? MucId { get; set; }

        public virtual Muc Muc { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppDbContext())
            {
                if (db.ChuyenMuc.Any(c => c.TieuDe == this.TieuDe && c.ChuyenMucId != this.ChuyenMucId))
                    yield return new ValidationResult("Tiêu đề chuyên mục đã tồn tại", new[] { "TieuDe" });
                if (db.ChuyenMuc.Any(c => c.Url == this.Url && c.ChuyenMucId != this.ChuyenMucId))
                    yield return new ValidationResult("URL chuyên mục đã tồn tại", new[] { "Url" });
            }
        }
    }
}
