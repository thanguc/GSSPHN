using EmptyWeb.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWeb.Services
{
    public class LoggingService
    {
        private readonly EntityContext _context;

        public LoggingService(EntityContext context)
        {
            _context = context;
        }

        public void WriteLog(string content)
        {
            _context.SystemLog.Add(new Models.SystemLog {
                Content = content
            });
            _context.SaveChanges();
        }
    }
}
