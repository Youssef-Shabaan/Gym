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
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<MemberSession> memberSessions { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<MemberPlan> memberPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----------------------
            // Member Gender
            // ----------------------
            modelBuilder.Entity<Member>()
                .Property(m => m.Gender)
                .HasConversion<string>()
                .HasMaxLength(50);

            // ----------------------
            // MemberSession (Many-to-Many)
            // ----------------------
            modelBuilder.Entity<MemberSession>()
                .HasOne(ms => ms.Member)
                .WithMany(m => m.memberSessions)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberSession>()
                .HasOne(ms => ms.Session)
                .WithMany(s => s.memberSessions)
                .HasForeignKey(ms => ms.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ----------------------
            // Attendance
            // ----------------------
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Session)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ----------------------
            // Session -> Trainer
            // ----------------------
            modelBuilder.Entity<Session>()
                .HasOne(s => s._Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);

            // ----------------------
            // MemberPlan (Member <-> Plan)
            // ----------------------
            modelBuilder.Entity<MemberPlan>()
                .HasOne(mp => mp.Member)
                .WithMany(m => m.memberPlans)
                .HasForeignKey(mp => mp.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberPlan>()
                .HasOne(mp => mp.Plan)
                .WithMany(p => p.MemberPlans)
                .HasForeignKey(mp => mp.PlanId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Session)
                .WithMany()
                .HasForeignKey(p => p.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete for Sessions
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Sessions)
                .WithOne(s => s.Plan)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete for MemberPlans
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.MemberPlans)
                .WithOne(mp => mp.Plan)
                .HasForeignKey(mp => mp.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Prevent deleting Trainer when deleting Plan
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Trainer)
                .WithMany(t => t.Plans)
                .HasForeignKey(p => p.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
