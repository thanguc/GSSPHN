using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmptyWeb.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}