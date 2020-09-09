using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Interfaces;
using SuperCube3D_DAL.Models;
using SuperCube3D_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Managers
{
    public class PlayerManager : UserManager<Player>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IPlayerAchievementRepository _playerAchievementRepository;
        private readonly IMapper _mapper;

        public PlayerManager(IUserStore<Player> store, IdentityFactoryOptions<PlayerManager> options,
            IMapper mapper, IAchievementRepository achievementRepository,
            IPlayerAchievementRepository playerAchievementRepository)
                : base(store)
        {
            _achievementRepository = achievementRepository;
            _playerAchievementRepository = playerAchievementRepository;
            _mapper = mapper;

            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<Player>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true,

            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
        }

        public async Task<IdentityResult> IncreaseSuccessfulLoginCount(Player player)
        {
            player.SuccessfulLoginCount += 1;

            var result = await UpdateAsync(player);

            var loginPlayerAchievement = _playerAchievementRepository.Get(player.Id, 1);

            if (player.SuccessfulLoginCount >= 3 && loginPlayerAchievement == null)
            {
                ActivateAchievement(player.Id, 1);
            }

            return result;
        }

        public List<AchievementModel> GetAchievements(Player player)
        {
            string id = player.Id;

            var achievements = _achievementRepository.GetAllByPlayerId(id);

            var result = _mapper.Map<List<AchievementModel>>(achievements);

            return result;
        }

        public void ActivateAchievement(string playerId, int achievementId)
        {
            _playerAchievementRepository.Create(playerId, achievementId);
        }
    }
}
