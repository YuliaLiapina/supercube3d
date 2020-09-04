using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public class ScoreRepository : RepositoryBase<Score>
    {
        public ScoreRepository() : base(new SuperCubeContext())
        {

        }
    }
}
