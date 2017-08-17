using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWeb.Models
{
    public class QueQuan
    {
        public int QueQuanID { get; set; }
        public string Ten { get; set; }

        public virtual ICollection<DangKyGiaSu> DangKyGiaSus { get; set; }
    }
}
