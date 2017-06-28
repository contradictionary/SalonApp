using System.Data.Entity;


namespace sqlexpressmvc.Models
{
    public class SalonModel : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}