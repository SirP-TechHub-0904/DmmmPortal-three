namespace DmmmPortal.Migrations
{
    using DmmmPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DmmmPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DmmmPortal.Models.ApplicationDbContext";
        }

        protected override void Seed(DmmmPortal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "SuperAdmin" };
                manager.Create(role);
            }

            //if (!context.Users.Any(u => u.UserName == "superadmin"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "superadmin", Email = "xyz@gmail.com" };
            //    manager.Create(user, "xyz@123");
            //    manager.AddToRole(user.Id, "SuperAdmin");
            //}

            //if (!context.Users.Any(u => u.UserName == "motherofmercy"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "motherofmercy", Email = "dmmm@gmail.com" };
            //    manager.Create(user, "ourlady");
            //    manager.AddToRole(user.Id, "Admin");
            //}
        }
    }
}
