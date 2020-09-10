using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SuperCube3D_DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Player : IdentityUser
    {
        public Player()
        {
            PlayerAchievements = new List<PlayerAchievement>();
            HighScores = new List<Score>();
        }

        public int SuccessfulLoginCount { get; set; }
        public bool PlayedTheGame { get; set; }
        public ICollection<Score> HighScores { get; set; }
        public ICollection<PlayerAchievement> PlayerAchievements { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Player> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}