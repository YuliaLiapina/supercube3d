using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Models
{
    public class PlayerAchievement
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
