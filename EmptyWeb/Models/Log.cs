using System;

namespace EmptyWeb.Models
{
    public class Log
    {
        public Guid LogId { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string UrlAccessed { get; set; }
        public string Message { get; set; }
    }
}
