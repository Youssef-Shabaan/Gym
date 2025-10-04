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
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<MemberSession> memberSessions { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Attendance> attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .Property(m => m.Gender)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            modelBuilder.Entity<MemberShip>()
                .Property(m => m.MemberShipType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            modelBuilder.Entity<Member>()
                .HasOne(m => m._MemberShip)
                .WithMany(s => s.Members)
                .HasForeignKey(m => m.MemberShipId);

             modelBuilder.Entity<MemberSession>()
                .HasKey(ms => new { ms.memberId, ms.sessionId });

            modelBuilder.Entity<Payment>()
               .Property(m => m.paymentMethod)
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasColumnType("nvarchar(50)");
            
            modelBuilder.Entity<Trainer>()
                .HasIndex(t => t.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
