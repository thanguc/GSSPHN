namespace EmptyWeb.Migrations
{
    using EmptyWeb.Contexts;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EmptyWeb.Contexts.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityContext context)
        {
        }
    }
}
