using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SuperCube3D_BL.Interfaces;
using SuperCube3D_BL.Managers;
using SuperCube3D_DAL;
using SuperCube3D_DAL.Interfaces;
using SuperCube3D_DAL.Models;
using SuperCube3D_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SuperCube3D_MVC.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<SuperCubeContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<PlayerManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.Register(c => new UserStore<Player>(c.Resolve<SuperCubeContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<PlayerManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("SuperCube3D_MVC")
            });

            builder.RegisterType<AchievementRepository>().As<IAchievementRepository>();
            builder.RegisterType<PlayerAchievementRepository>().As<IPlayerAchievementRepository>();

            builder.RegisterType<ScoreManager>().As<IScoreManager>();
            builder.RegisterType<ScoreRepository>().As<IScoreRepository>();
            builder.RegisterModule<MapperModule>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}