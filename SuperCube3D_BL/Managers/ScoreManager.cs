using AutoMapper;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Interfaces;
using SuperCube3D_DAL.Models;
using SuperCube3D_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Managers
{
    public class ScoreManager : IScoreManager
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;

        public ScoreManager(IMapper mapper, IScoreRepository scoreRepository)
        {
            _mapper = mapper;
            _scoreRepository = scoreRepository;
        }

        public IList<ScoreModel> GetAllScores()
        {
            var scoreList = _scoreRepository.GetAll();

            var result = _mapper.Map<IList<ScoreModel>>(scoreList);

            return result;
        }

        public void CreateScore(ScoreModel scoreModel)
        {
            var score = _mapper.Map<Score>(scoreModel);

            _scoreRepository.Create(score);
        }
    }
}
