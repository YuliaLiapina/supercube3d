using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SuperCube3D_DAL.Models;

namespace SuperCube3D_DAL
{
    public class SuperCubeContext : IdentityDbContext<Player>
    {
        public SuperCubeContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CustomInitializer());
        }

        public DbSet<Score> HighScores { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<PlayerAchievement> PlayerAchievements { get; set; }

        //public static SuperCubeContext Create()
        //{
        //    return new SuperCubeContext();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Score>()
                .ToTable("HighScores");

            modelBuilder.Entity<Player>()
                .HasMany<Score>(p => p.HighScores)
                .WithRequired(s => s.Player)
                .HasForeignKey(s => s.PlayerId);

            modelBuilder.Entity<PlayerAchievement>()
                .HasKey(pa => new
                {
                    pa.PlayerId, pa.AchievementId
                });

            modelBuilder.Entity<PlayerAchievement>()
                .HasRequired(pa => pa.Achievement)
                .WithMany(a => a.PlayerAchievements)
                .HasForeignKey(pa => pa.AchievementId);

            modelBuilder.Entity<PlayerAchievement>()
                .HasRequired(pa => pa.Player)
                .WithMany(p => p.PlayerAchievements)
                .HasForeignKey(pa => pa.PlayerId);
        }
    }
}
