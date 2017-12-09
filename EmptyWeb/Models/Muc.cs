using EmptyWeb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmptyWeb.Models
{
    public class Muc : IValidatableObject
    {
        public Guid MucId { get; set; } = Guid.NewGuid();
        public string TieuDe { get; set; }
        public bool IsHidden { get; set; }
        public int SortNumber { get; set; }

        public virtual ICollection<ChuyenMuc> ChuyenMucs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppDbContext())
            {
                if (db.Muc.Any(m => m.TieuDe == this.TieuDe && m.MucId != this.MucId))
                    yield return new ValidationResult("Tiêu đề mục đã tồn tại", new[] { "TieuDe" });
            }
        }
    }
}
