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

namespace SuperCube3D_BL.Managers
{
    public class PlayerManager : UserManager<Player>
    {
        //create playerRepository maybe?
        private readonly AchievementRepository _achievementRepository;
        private readonly PlayerAchievementRepository _playerAchievementRepository;

        public PlayerManager(IUserStore<Player> store)
                : base(store)
        {
            _achievementRepository = new AchievementRepository();
            _playerAchievementRepository = new PlayerAchievementRepository();
        }

        public static PlayerManager Create(IdentityFactoryOptions<PlayerManager> options, IOwinContext context)
        {
            SuperCubeContext db = context.Get<SuperCubeContext>();
            var manager = new PlayerManager(new UserStore<Player>(db));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Player>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true,

            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }

        public async Task<IdentityResult> IncreaseSuccessfulLoginCount(Player player)
        {
            player.SuccessfulLoginCount += 1;

            var result = await UpdateAsync(player);

            //var loginAchievement = player.Achievements.FirstOrDefault(ach => ach.Id == 1);

            var loginPlayerAchievement = _playerAchievementRepository.Get(player.Id, 1);

            if (player.SuccessfulLoginCount >= 3 && loginPlayerAchievement == null)
            {
                //await ActivateAchievement(player, 1);
                _playerAchievementRepository.Create(player.Id, 1);
            }

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
