using Autofac;
using AutoMapper;
using SuperCube3D_BL.Models;
using SuperCube3D_DAL.Models;
using SuperCube3D_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperCube3D_MVC.Autofac
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //maps here
                cfg.CreateMap<Achievement, AchievementModel>();
                cfg.CreateMap<AchievementModel, AchievementViewModel>();
                cfg.CreateMap<AchievementViewModel, AchievementModel>();
                cfg.CreateMap<AchievementModel, Achievement>();

                cfg.CreateMap<Score, ScoreModel>();
                cfg.CreateMap<ScoreModel, ScoreViewModel>();
                cfg.CreateMap<ScoreViewModel, ScoreModel>();
                cfg.CreateMap<ScoreModel, Score>();
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}