using SuperCube3D_DAL.Interfaces;
using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Models
{
    public class ScoreModel
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }

        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
