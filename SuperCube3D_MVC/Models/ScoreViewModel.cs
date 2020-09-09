using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperCube3D_MVC.Models
{
    public class ScoreViewModel
    {
        public int Result { get; set; }
        public DateTime Date { get; set; }

        public Player Player { get; set; }
    }
}