using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public class AchievementRepository : RepositoryBase<Achievement>
    {
        public AchievementRepository() : base(new SuperCubeContext())
        {

        }
    }
}
