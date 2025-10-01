using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Gym.DAL.DataBase
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }
        public DbSet<Member> members { get; set; }
        public DbSet<MemberShip> memberShips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>()
                .HasOne(m => m._MemberShip)
                .WithMany(s => s.Members)
                .HasForeignKey(m => m.MemberShipId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
