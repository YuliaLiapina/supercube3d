using SuperCube3D_DAL.Interfaces;
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
    public class AchievementRepository : IAchievementRepository
    {
        public IEnumerable<Achievement> GetAllByPlayerId(string playerId)
        {
            using (var ctx = new SuperCubeContext())
            {
                var playerAchievements = ctx.PlayerAchievements.Where(pa => pa.PlayerId == playerId).ToList();
                var achievements = ctx.Achievements.ToList();

                var result = achievements.Join(playerAchievements,
                    a => a.Id,
                    pa => pa.AchievementId,
                    (a, pa) => new Achievement
                    {
                        Id = a.Id,
                        Description = a.Description,
                        Title = a.Title
                    })
                    .ToList();

                return result;
            }
        }

        public Achievement Get(int id)
        {
            using (var ctx = new SuperCubeContext())
            {
                var achievements = ctx.Achievements.Include(ach => ach.PlayerAchievements);

                return achievements.FirstOrDefault(ach => ach.Id.Equals(id));
            }
        }

        //for admin maybe
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
