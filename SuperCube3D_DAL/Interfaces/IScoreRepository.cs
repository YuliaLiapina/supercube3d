using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Interfaces
{
    public interface IScoreRepository
    {
        IEnumerable<Score> GetAll();
        void Create(Score score);
    }
}
