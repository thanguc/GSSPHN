using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWeb.Models
{
    public class Muc
    {
        public Guid MucId { get; set; } = Guid.NewGuid();
        public string TieuDe { get; set; }
        public bool IsHidden { get; set; }
        public int SortNumber { get; set; }

        public virtual ICollection<ChuyenMuc> ChuyenMucs { get; set; }
    }
}
