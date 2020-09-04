using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
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

            //builder.RegisterType<ScoreRepository>().As<RepositoryBase<Score>>();
            //builder.RegisterType<AnimalsManager>().As<IAnimalsManager>();
            //builder.RegisterType<JsonConverter>().As<IJsonConverter>();

            //builder.RegisterModule<MapperModule>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}