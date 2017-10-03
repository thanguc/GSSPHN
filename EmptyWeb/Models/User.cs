using System;

namespace EmptyWeb.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid UserroleID { get; set; }

    }
}