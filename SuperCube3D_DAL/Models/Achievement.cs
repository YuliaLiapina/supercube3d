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
            Players = new List<Player>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
