using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Interfaces
{
    public interface IPlayerAchievementRepository
    {
        IEnumerable<PlayerAchievement> GetAllByPlayerId(string playerId);
        PlayerAchievement Get(string playerId, int achievementId);
        void Create(string playerId, int achievementId);
    }
}
