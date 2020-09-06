using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Models;
using SuperCube3D_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SuperCube3D_MVC.Controllers.API
{
    public class ScoreController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IScoreManager _scoreManager;
        private readonly UserManager<Player> _playerManager;

        public ScoreController(IMapper mapper, IScoreManager scoreManager, UserManager<Player> playerManager)
        {
            _mapper = mapper;
            _scoreManager = scoreManager;
            _playerManager = playerManager;
        }

        // GET: api/Score
        public string Get()
        {
            var scoreList = _scoreManager.GetAllScores();

            var result = _mapper.Map<List<ScorePostModel>>(scoreList);

            return JsonConvert.SerializeObject(result);
        }

        // GET: api/Score/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        public void Post([FromBody] JObject unityScoreJson)
        {
            //need to test asap!

            var unityScore = unityScoreJson.ToObject<ScorePostModel>();

            var player = _playerManager.FindById(User.Identity.GetUserId());

            unityScore.Player = player;
            unityScore.Date = DateTime.Now;

            var result = _mapper.Map<ScoreModel>(unityScore);

            _scoreManager.CreateScore(result);
        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
        }
    }
}
