using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Gym.DAL.DataBase
{
    public class GymDbContext : IdentityDbContext<User>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<MemberShip> memberShips { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<MemberSession> memberSessions { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<TrainerSubscription> trainerSubscriptions { get; set; }

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
                .HasOne(ms => ms.Member)
                .WithMany(m => m.memberSessions)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberSession>()
                .HasOne(ms => ms.Session)
                .WithMany(s => s.memberSessions)
                .HasForeignKey(ms => ms.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Session)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
               .HasOne(m => m._MemberShip)
               .WithMany(s => s.Members)
               .HasForeignKey(m => m.MemberShipId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Session>()
                .HasOne(s => s._Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberSession>()
                .HasOne(ms => ms.Member)
                .WithMany(m => m.memberSessions)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberSession>()
                .HasOne(ms => ms.Session)
                .WithMany(s => s.memberSessions)
                .HasForeignKey(ms => ms.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Session)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrainerSubscription>()
                .HasOne(ts => ts.Trainer)
                .WithMany(t => t.TrainerSubscriptions)
                .HasForeignKey(ts => ts.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrainerSubscription>()
                .HasOne(ts => ts.Member)
                .WithMany(m => m.TrainerSubscriptions)
                .HasForeignKey(ts => ts.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrainerSubscription>()
                .HasOne(ts => ts.Payment)
                .WithOne(p => p.TrainerSubscription)
                .HasForeignKey<TrainerSubscription>(ts => ts.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
    }
}
