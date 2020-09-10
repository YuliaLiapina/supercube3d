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
        private readonly IPlayerAchievementRepository _playerAchievementRepository;
        private readonly IMapper _mapper;
        private readonly PlayerManager _playerManager;

        public ScoreManager(IMapper mapper, IScoreRepository scoreRepository, PlayerManager playerManager,
            IPlayerAchievementRepository playerAchievementRepository)
        {
            _mapper = mapper;
            _scoreRepository = scoreRepository;
            _playerManager = playerManager;
            _playerAchievementRepository = playerAchievementRepository;
        }

        public IList<ScoreModel> GetTop10Scores()
        {
            var scoreList = _scoreRepository.GetAll();

            var top10List = scoreList.OrderByDescending(s => s.Result).Take(10);

            var result = _mapper.Map<IList<ScoreModel>>(top10List);

            return result;
        }

        public ScoreModel GetHighScoreForPlayer(Player player)
        {
            string id = player.Id;

            var playerScores = _scoreRepository.GetAll().Where(s => s.PlayerId == id);

            var highScore = playerScores.OrderByDescending(s => s.Result).FirstOrDefault();

            var result = _mapper.Map<ScoreModel>(highScore);

            return result;
        }

        public void CreateScore(ScoreModel scoreModel)
        {
            var score = _mapper.Map<Score>(scoreModel);

            var topScore = GetTop10Scores().FirstOrDefault();
            var topScorePlayerAchievement = _playerAchievementRepository.Get(score.PlayerId, 3);

            if (score.Result > topScore.Result && topScorePlayerAchievement == null)
            {
                _playerAchievementRepository.Create(score.PlayerId, 3);
            }

            var score3000PlayerAchievement = _playerAchievementRepository.Get(score.PlayerId, 2);

            if (score.Result >= 3000 && score3000PlayerAchievement == null)
            {
                _playerAchievementRepository.Create(score.PlayerId, 2);
            }

            _scoreRepository.Create(score);
        }
    }
}
