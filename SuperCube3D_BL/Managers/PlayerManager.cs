using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SuperCube3D_DAL;
using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using SuperCube3D_DAL.Repositories;
using SuperCube3D_BL.Models;
using AutoMapper;

namespace SuperCube3D_BL.Managers
{
    public class PlayerManager : UserManager<Player>
    {
        private readonly AchievementRepository _achievementRepository;
        private readonly PlayerAchievementRepository _playerAchievementRepository;
        private readonly IMapper _mapper;

        public PlayerManager(IUserStore<Player> store, IdentityFactoryOptions<PlayerManager> options,
            IMapper mapper)
                : base(store)
        {
            _achievementRepository = new AchievementRepository();
            _playerAchievementRepository = new PlayerAchievementRepository();
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
                _playerAchievementRepository.Create(player.Id, 1);
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

        //private async Task<IdentityResult> ActivateAchievement(Player player, int achievementId)
        //{
        //    var achievement = _achievementRepository.Get(achievementId);

        //    if (achievement != null)
        //    {
        //        //player.PlayerAchievements.Add(achievement);
        //    }

        //    var result = await UpdateAsync(player);

        //    return result;
        //}
    }
}
