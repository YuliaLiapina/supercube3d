using Microsoft.AspNet.Identity;
using SuperCube3D_BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperCube3D_MVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly PlayerManager _playerManager;

        public GameController(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        // GET: Game
        public async Task<ActionResult> Index()
        {
            var user = await _playerManager.FindByIdAsync(User.Identity.GetUserId());

            if (!user.PlayedTheGame)
            {
                await _playerManager.MarkPlayedGameTrue(user);
            }

            return View();
        }
    }
}