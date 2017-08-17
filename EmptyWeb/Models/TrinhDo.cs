using System.Collections.Generic;

namespace EmptyWeb.Models
{
    public class TrinhDo
    {
        public int TrinhDoId { get; set; }
        public string TenTrinhDo { get; set; }

        public virtual ICollection<DangKyGiaSu> DangKyGiaSus { get; set; }
        public virtual ICollection<TimGiaSu> TimGiaSus { get; set; }
    }
}
