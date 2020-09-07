using AutoMapper;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Models;
using SuperCube3D_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Managers
{
    public class PlayerService : IPlayerService
    {
        private readonly AchievementRepository _achievementRepository;
        private readonly PlayerAchievementRepository _playerAchievementRepository;
        private readonly IMapper _mapper;

        public PlayerService(IMapper mapper)
        {
            _mapper = mapper;
            _achievementRepository = new AchievementRepository();
            _playerAchievementRepository = new PlayerAchievementRepository();
        }

        public List<AchievementModel> GetAchievements(Player player)
        {
            string id = player.Id;

            var achievements = _achievementRepository.GetAllByPlayerId(id);

            var result = _mapper.Map<List<AchievementModel>>(achievements);

            return result;
        }
    }
}
