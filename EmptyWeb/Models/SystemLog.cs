using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWeb.Models
{
    public class SystemLog
    {
        public Guid SystemLogId { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
