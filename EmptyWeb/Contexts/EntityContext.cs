using EmptyWeb.Models;
using System.Data.Entity;
using System.Linq;

namespace EmptyWeb.Contexts
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("EntityContext")
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<DangKyGiaSu> DangKyGiaSu { get; set; }
        public DbSet<TimGiaSu> TimGiaSu { get; set; }
        public DbSet<Userrole> Userrole { get; set; }
        public DbSet<QueQuan> QueQuan { get; set; }
        public DbSet<TrinhDo> TrinhDo { get; set; }
        public DbSet<Muc> Muc { get; set; }
        public DbSet<ChuyenMuc> ChuyenMuc { get; set; }

        public bool IsValid { get { return this.GetValidationErrors().Count() == 0; } }

        public void SaveObject(params object[] obj)
        {
            foreach (object o in obj)
            {
                this.Entry(o).State = EntityState.Modified;
            }
            this.SaveChanges();
        }

        public void DeleteObject(params object[] obj)
        {
            foreach (object o in obj)
            {
                this.Entry(o).State = EntityState.Deleted;
            }
            this.SaveChanges();
        }
    }
}