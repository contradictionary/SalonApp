using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(sqlexpressmvc.Startup))]
namespace sqlexpressmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          Database.SetInitializer(new DropCreateDatabaseIfModelChanges<sqlexpressmvc.Models.SalonContext>());

        }

    }
}
