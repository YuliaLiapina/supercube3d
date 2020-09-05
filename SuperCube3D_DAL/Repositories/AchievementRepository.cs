using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public class AchievementRepository
    {
        //public AchievementRepository() : base(new SuperCubeContext())
        //{

        //}

        public Achievement Get(int id)
        {
            using (var ctx = new SuperCubeContext())
            {
                var achievements = ctx.Achievements.Include(ach => ach.Players);

                return achievements.FirstOrDefault(ach => ach.Id.Equals(id));
            }
        }

        public void Update(Achievement achievement)
        {
            using (var ctx = new SuperCubeContext())
            {
                ctx.Achievements.AddOrUpdate(achievement);

                ctx.SaveChanges();
            }
        }
    }
}
