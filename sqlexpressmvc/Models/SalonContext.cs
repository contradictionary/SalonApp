using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sqlexpressmvc.Models
{
    public class SalonContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SalonContext() : base("name=SalonContext")
        {
            Database.SetInitializer<SalonContext>(new CreateDatabaseIfNotExists<SalonContext>());
        }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.RateList> RateLists { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.Employees> Employees { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.Stock> Stocks { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.MembershipBill> MembershipBills { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.Billing> Billings { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.UsedProducts> UsedProducts { get; set; }

        public System.Data.Entity.DbSet<sqlexpressmvc.Models.UpdateVolume> UpdateVolume { get; set; }
    }
}
