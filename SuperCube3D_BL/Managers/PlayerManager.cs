using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SuperCube3D_DAL;
using SuperCube3D_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_BL.Managers
{
    public class PlayerManager : UserManager<Player>
    {
        public PlayerManager(IUserStore<Player> store)
                : base(store)
        {
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
    }
}
