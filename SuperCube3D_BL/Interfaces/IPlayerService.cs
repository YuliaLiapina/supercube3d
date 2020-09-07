using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Interfaces
{
    public interface IPlayerService
    {
        List<AchievementModel> GetAchievements(Player player);
    }
}
