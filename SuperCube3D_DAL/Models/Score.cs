using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }

        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
