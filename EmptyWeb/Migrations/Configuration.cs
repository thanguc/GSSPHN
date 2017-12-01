namespace EmptyWeb.Migrations
{
    using EmptyWeb.Data;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EmptyWeb.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
        }
    }
}
