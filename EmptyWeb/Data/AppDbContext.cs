using EmptyWeb.Models;
using System.Data.Entity;

namespace EmptyWeb.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<DangKyGiaSu> DangKyGiaSu { get; set; }
        public DbSet<TimGiaSu> TimGiaSu { get; set; }
        public DbSet<Userrole> Userrole { get; set; }
        public DbSet<QueQuan> QueQuan { get; set; }
        public DbSet<TrinhDo> TrinhDo { get; set; }
        public DbSet<SystemLog> SystemLog { get; set; }
    }
}