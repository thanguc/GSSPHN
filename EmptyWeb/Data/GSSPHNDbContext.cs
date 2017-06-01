using EmptyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmptyWeb.Data
{
    public class GSSPHNDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<DangKyGiaSu> DangKyGiaSu { get; set; }
    }
}