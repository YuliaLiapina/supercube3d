using SuperCube3D_DAL.Interfaces;
using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        public IEnumerable<Score> GetAll()
        {
            using (var ctx = new SuperCubeContext())
            {
                return ctx.HighScores.Include(s => s.Player).ToList();
            }
        }

        public void Create(Score score)
        {
            using (var ctx = new SuperCubeContext())
            {
                ctx.HighScores.Add(score);

                ctx.SaveChanges();
            }
        }
    }
}
