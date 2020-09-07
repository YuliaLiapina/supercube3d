using SuperCube3D_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Models
{
    public class Achievement : IEntity
    {
        public Achievement()
        {
            PlayerAchievements = new List<PlayerAchievement>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<PlayerAchievement> PlayerAchievements { get; set; }
    }
}
