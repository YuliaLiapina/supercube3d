using SuperCube3D_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Interfaces
{
    public interface IScoreManager
    {
        IList<ScoreModel> GetTop10Scores();
        void CreateScore(ScoreModel scoreModel);
    }
}
