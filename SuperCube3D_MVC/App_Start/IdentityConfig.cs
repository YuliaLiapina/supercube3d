using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SuperCube3D_BL.Managers;
using SuperCube3D_DAL;
using SuperCube3D_DAL.Models;
using SuperCube3D_MVC.Models;

namespace SuperCube3D_MVC
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<Player, string>
    {
        public ApplicationSignInManager(PlayerManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Player user)
        {
            return user.GenerateUserIdentityAsync((PlayerManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<PlayerManager>(), context.Authentication);
        }
    }
}
