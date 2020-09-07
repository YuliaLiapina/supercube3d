using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public class PlayerAchievementRepository
    {
        public PlayerAchievement Get(string playerId, int achievementId)
        {
            using (var ctx = new SuperCubeContext())
            {
                return ctx.PlayerAchievements.FirstOrDefault(pa => pa.PlayerId == playerId && pa.AchievementId == achievementId);
            }
        }

        public void Create(string playerId, int achievementId)
        {
            using (var ctx = new SuperCubeContext())
            {
                var itemToAdd = new PlayerAchievement
                {
                    PlayerId = playerId,
                    AchievementId = achievementId
                };

                ctx.PlayerAchievements.Add(itemToAdd);

                ctx.SaveChanges();
            }
        }
    }
}
