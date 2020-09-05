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

        public static SuperCubeContext Create()
        {
            return new SuperCubeContext();
        }
    }
}
