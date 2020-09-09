using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_MVC.Filters;
using SuperCube3D_MVC.Models;

namespace SuperCube3D_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScoreManager _scoreManager;
        private readonly IMapper _mapper;

        public HomeController(IScoreManager scoreManager, IMapper mapper)
        {
            _scoreManager = scoreManager;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scores()
        {
            var top10Scores = _scoreManager.GetTop10Scores();

            var model = _mapper.Map<List<ScoreViewModel>>(top10Scores);

            return View(model);
        }
    }
}