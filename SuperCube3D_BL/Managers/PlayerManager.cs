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

        public PlayerManager(IUserStore<Player> store)
                : base(store)
        {
            _achievementRepository = new AchievementRepository();
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

        public async Task<IdentityResult> IncreaseSuccessfulLoginCount(IOwinContext context, Player player)
        {
            var dbCtx = context.Get<SuperCubeContext>();

            player.SuccessfulLoginCount += 1;

            //var loginAchievement = player.Achievements.FirstOrDefault(ach => ach.Id == 1);

            var loginAchievement = _achievementRepository.Get(1);

            if (player.SuccessfulLoginCount >= 3 && loginAchievement.Players.Contains(player) == false)
            {
                ActivateAchievement(dbCtx, player, 1);
            }

            var result = await UpdateAsync(player);

            dbCtx.SaveChanges();

            return result;
        }

        private void ActivateAchievement(SuperCubeContext context, Player player, int achievementId)
        {
            var achievement = _achievementRepository.Get(achievementId);

            if (achievement != null)
            {
                //dynamic proxy player is added

                achievement.Players.Add(player);

                //_achievementRepository.Update(achievement);

                context.SaveChanges();
            }
        }
    }
}
