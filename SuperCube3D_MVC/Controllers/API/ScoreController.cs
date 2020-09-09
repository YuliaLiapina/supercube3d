using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_BL.Managers;
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
using Microsoft.AspNet.Identity.Owin;

namespace SuperCube3D_MVC.Controllers.API
{
    [Authorize]
    public class ScoreController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IScoreManager _scoreManager;
        private readonly PlayerManager _playerManager;

        public ScoreController(IMapper mapper, IScoreManager scoreManager, PlayerManager playerManager)
        {
            _mapper = mapper;
            _scoreManager = scoreManager;
            _playerManager = playerManager;
        }

        // GET: api/Score
        public string Get()
        {
            var scoreList = _scoreManager.GetTop10Scores();

            var result = _mapper.Map<List<ScorePostModel>>(scoreList);

            return JsonConvert.SerializeObject(result);
        }

        // GET: api/Score/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Score
        public void Post([FromBody] JObject unityScoreJson)
        {
            var unityScore = unityScoreJson.ToObject<ScorePostModel>();

            unityScore.PlayerId = User.Identity.GetUserId();
            unityScore.Date = DateTime.Now;

            var result = _mapper.Map<ScoreModel>(unityScore);

            _scoreManager.CreateScore(result);
        }

        // DELETE: api/Score/5
        public void Delete(int id)
        {
        }
    }
}
